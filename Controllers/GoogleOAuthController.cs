using LineOfficial_MVC.Keys;
using LineOfficial_MVC.Models;
using LineOfficial_MVC.Models.Google;
using LineOfficial_MVC.ViewModels.GoogleOAuth;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text.Json.Nodes;

namespace LineOfficial_MVC.Controllers
{
    public class GoogleOAuthController : Controller
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
        public GoogleOAuthController(ILogger<LineController> logger, AppSettings appSettings)
        {
            _logger = logger;
            _appSettings = appSettings;
        }
        #endregion

        /// <summary>
        /// Google OAuth登入 首頁
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("[controller]/[action]")]
        public async Task<IActionResult> Callback(string code)
        {
            FinishViewModel viewModel = new FinishViewModel();
            viewModel.authorizationCode = code;

            try
            {
                // 使用授權碼 code 向 Google 取得用戶資料
                string grantType = _appSettings.GoogleOAuth.grantType;
                string clientId = _appSettings.GoogleOAuth.clientId;
                string clientSecret = _appSettings.GoogleOAuth.clientSecret;
                string redirectUri = _appSettings.GoogleOAuth.redirectUri;

                #region 發送API取得token
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
                    _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json")); //添加 accept header
                    string tokenUrl = GoogleOAuthApiUrl.accessTokenUrl;
                    var response = _httpClient.PostAsync(tokenUrl, formContent).Result;

                    var jsonResponse = response.Content.ReadAsStringAsync().Result;

                    // 擷取回傳參數節點
                    var resultJsonNode = JsonNode.Parse(jsonResponse);
                    string accessToken = resultJsonNode["access_token"].GetValue<string>();
                    string idToken = resultJsonNode["id_token"].GetValue<string>();
                    viewModel.accessToken = accessToken;
                    viewModel.idToken = idToken;

                    // 反解回傳參數
                    var result = JsonConvert.DeserializeObject<TokenResponse>(jsonResponse);
                }
                #endregion

                #region 驗證及解析 IDToken
                using (HttpClient _httpClient = new HttpClient())
                {
                    _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json")); //添加 accept header
                    //_httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {viewModel.accessToken}");
                    string apiurl = $"{GoogleOAuthApiUrl.decodeIDTokenUrl}";
                    apiurl += viewModel.idToken;
                    var response = _httpClient.GetAsync(apiurl).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        var jsonResponse = response.Content.ReadAsStringAsync().Result;
                        var result = JsonConvert.DeserializeObject<DecodeIDTokenResponse>(jsonResponse);

                        viewModel.userId = result.sub;
                        viewModel.email = result.email;
                        viewModel.name = result.name;
                        viewModel.pictureUrl = result.picture;
                    }
                }
                #endregion

                #region 發送API取得UserInfo by AccessToken
                //using (HttpClient _httpClient = new HttpClient())
                //{
                //    _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json")); //添加 accept header
                //    _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {viewModel.accessToken}");
                //    string apiurl = $"{GoogleOAuthApiUrl.getUserInformationUrl}";
                //    var response = _httpClient.GetAsync(GoogleOAuthApiUrl.getUserInformationUrl).Result;
                //    if (response.IsSuccessStatusCode)
                //    {
                //        var jsonResponse = response.Content.ReadAsStringAsync().Result;
                //        var result = JsonConvert.DeserializeObject<UserInfoResponse>(jsonResponse);

                //        viewModel.userId = result.sub;
                //        viewModel.email = result.email;
                //        viewModel.name = result.name;
                //        viewModel.pictureUrl = result.picture;
                //    }
                //}
                #endregion

                return RedirectToAction("Finish", "GoogleOAuth", viewModel);
            }
            catch (Exception ex)
            {
                // 處理例外錯誤
                return RedirectToAction("Index", "GoogleOAuth");
            }
        }


        /// <summary>
        /// Google 登入完成頁
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Finish(FinishViewModel viewModel)
        {
            return View(viewModel);
        }

    }
}
