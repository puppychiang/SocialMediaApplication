using LineOfficial_MVC.Keys;
using LineOfficial_MVC.Models;
using LineOfficial_MVC.Models.Line;
using LineOfficial_MVC.ViewModels.LineOAuth;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace LineOfficial_MVC.Controllers
{
    public class LineOAuthController : Controller
    {
        private readonly ILogger<LineController> _logger;
        /// <summary>
        /// AppSettings
        /// </summary>
        private readonly AppSettings _appSettings;

        #region Constructor
        /// <summary>
        /// 建構子
        /// </summary>
        /// <param name="logger"></param>
        public LineOAuthController(ILogger<LineController> logger, AppSettings appSettings)
        {
            _logger = logger;
            _appSettings = appSettings;
        }
        #endregion

        /// <summary>
        /// Line OAuth 首頁
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Line 回傳 授權碼 (Authorization Code)
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("[controller]/[action]")]
        public async Task<IActionResult> Callback(string code)
        {
            FinishViewModel viewModel = new FinishViewModel();
            viewModel.authorizationCode = code;

            TokenResponse tokenResponse = new TokenResponse();
            InformationResponse informationResponse = new InformationResponse();
            ProfileResponse profileResponse = new ProfileResponse();
            DecodeIDTokenResponse decodeIDTokenResponse = new DecodeIDTokenResponse();

            try
            {
                // 使用授權碼 code 向 Line 取用戶資料
                string grantType = _appSettings.LineOAuth.grantType;
                string clientId = _appSettings.LineOAuth.clientId;
                string clientSecret = _appSettings.LineOAuth.clientSecret;
                string redirectUri = _appSettings.LineOAuth.redirectUri;

                #region 發送API取得 Access Token，以授權碼(Authorization Code)獲取 Access Token、ID Token
                using (HttpClient _httpClient = new HttpClient())
                {
                    //利用key-value塞入Header參數
                    var formContent = new FormUrlEncodedContent(new[]
                    {
                        new KeyValuePair<string, string>("grant_type", grantType),
                        new KeyValuePair<string, string>("code", code),
                        new KeyValuePair<string, string>("redirect_uri",redirectUri),
                        new KeyValuePair<string, string>("client_id", clientId),
                        new KeyValuePair<string, string>("client_secret", clientSecret),
                    });
                    //_httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));
                    _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json")); //添加 accept header
                    string tokenUrl = LineOAuthApiUrl.accessTokenUrl;
                    var response = _httpClient.PostAsync(tokenUrl, formContent).Result;

                    var jsonResponse = response.Content.ReadAsStringAsync().Result;
                    tokenResponse = JsonConvert.DeserializeObject<TokenResponse>(jsonResponse);

                    viewModel.accessToken = tokenResponse.access_token;
                }
                #endregion

                #region 發送API取得 id_token 反解資料
                using (HttpClient _httpClient = new HttpClient())
                {
                    if (!string.IsNullOrEmpty(tokenResponse.access_token))
                    {
                        //利用key-value塞入參數
                        var formContent = new FormUrlEncodedContent(new[]
                        {
                            new KeyValuePair<string, string>("id_token", tokenResponse.id_token),
                            new KeyValuePair<string, string>("client_id", clientId),
                        });

                        _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json")); //添加 accept header
                        string verifyIDTokenUrl = LineOAuthApiUrl.decodeIDTokenUrl;

                        //發送API取得profile
                        var response = _httpClient.PostAsync(verifyIDTokenUrl, formContent).Result;
                        if (response.IsSuccessStatusCode)
                        {
                            var jsonResponse = response.Content.ReadAsStringAsync().Result;
                            decodeIDTokenResponse = JsonConvert.DeserializeObject<DecodeIDTokenResponse>(jsonResponse);

                            //紀錄使用者資訊
                            viewModel.userId = decodeIDTokenResponse.sub;
                            viewModel.email = decodeIDTokenResponse.email;
                            viewModel.pictureUrl = decodeIDTokenResponse.picture;
                            return RedirectToAction("Finish", "LineOAuth", viewModel);
                        }
                    }
                }
                #endregion

                #region 發送API取得User Profile
                //using (HttpClient _httpClient = new HttpClient())
                //{
                //    if (!string.IsNullOrEmpty(tokenResponse.access_token))
                //    {
                //        string profileUrl = LineOAuthApiUrl.getUserProfileUrl;
                //        _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {tokenResponse.access_token}");
                //        //發送API取得profile
                //        var response = _httpClient.GetAsync(profileUrl).Result;
                //        if (response.IsSuccessStatusCode)
                //        {
                //            var jsonResponse = response.Content.ReadAsStringAsync().Result;
                //            profileResponse = JsonConvert.DeserializeObject<ProfileResponse>(jsonResponse);

                //            //紀錄使用者profile
                //            viewModel.userId = profileResponse.userId;
                //            viewModel.pictureUrl = profileResponse.pictureUrl;
                //        }
                //    }
                //}
                #endregion

                return RedirectToAction("Finish", "LineOAuth", viewModel);
            }
            catch (Exception ex)
            {
                // 處理例外錯誤
                return RedirectToAction("Index", "LineOAuth", viewModel);
            }
        }

        /// <summary>
        /// Line 登入完成頁
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Finish(FinishViewModel viewModel)
        {
            return View(viewModel);
        }

    }
}
