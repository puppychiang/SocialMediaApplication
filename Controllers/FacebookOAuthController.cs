using LineOfficial_MVC.Keys;
using LineOfficial_MVC.Models;
using LineOfficial_MVC.Models.Facebook;
using LineOfficial_MVC.ViewModels.FacebookOAuth;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text.Json.Nodes;

namespace LineOfficial_MVC.Controllers
{
    public class FacebookOAuthController : Controller
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
        public FacebookOAuthController(ILogger<LineController> logger, AppSettings appSettings)
        {
            _logger = logger;
            _appSettings = appSettings;
        }
        #endregion

        /// <summary>
        /// Facebook OAuth登入 首頁
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
                // 使用授權碼 code 向 Facebook 取得用戶資料
                string clientId = _appSettings.FacebookOAuth.clientId;
                string clientSecret = _appSettings.FacebookOAuth.clientSecret;
                string redirectUri = _appSettings.FacebookOAuth.redirectUri;

                #region 發送API取得accessToken
                using (HttpClient _httpClient = new HttpClient())
                {
                    _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json")); //添加 accept header
                    string tokenUrl = FacebookOAuthApiUrl.accessTokenUrl;
                    tokenUrl += $"?client_id={clientId}";
                    tokenUrl += $"&redirect_uri={redirectUri}";
                    tokenUrl += $"&client_secret={clientSecret}";
                    tokenUrl += $"&code={code}";

                    var response = _httpClient.GetAsync(tokenUrl).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        var jsonResponse = response.Content.ReadAsStringAsync().Result;
                        var result = JsonConvert.DeserializeObject<TokenResponse>(jsonResponse);

                        viewModel.accessToken = result.access_token;
                    }  
                }
                #endregion

                #region 取得 UserInfo
                using (HttpClient _httpClient = new HttpClient())
                {
                    _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json")); //添加 accept header
                    string apiurl = $"{FacebookOAuthApiUrl.getUserInformationUrl}";
                    apiurl += viewModel.accessToken;
                    var response = _httpClient.GetAsync(apiurl).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        var jsonResponse = response.Content.ReadAsStringAsync().Result;
                        var resultNode = JsonNode.Parse(jsonResponse);
                        var result = JsonConvert.DeserializeObject<UserInfoResponse>(jsonResponse);

                        viewModel.userId = result.id;
                        viewModel.email = result.email;
                        viewModel.name = result.name;
                        //viewModel.pictureUrl = result.picture;
                    }
                }
                #endregion

                return RedirectToAction("Finish", "FacebookOAuth", viewModel);
            }
            catch (Exception ex)
            {
                // 處理例外錯誤
                return RedirectToAction("Index", "FacebookOAuth");
            }
        }


        /// <summary>
        /// Facebook 登入完成頁
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Finish(FinishViewModel viewModel)
        {
            return View(viewModel);
        }

    }
}
