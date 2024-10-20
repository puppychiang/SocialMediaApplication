using LineOfficial_MVC.Models;
using LineOfficial_MVC.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Data;

namespace LineOfficial_MVC.Controllers
{
    public class LiffController : Controller
    {
        private readonly ILogger<LiffController> _logger;

        /// <summary>
        /// 建構子
        /// </summary>
        public LiffController(ILogger<LiffController> logger)
        {
            _logger = logger;
        }

        #region 綁定功能
        /// <summary>
        /// Line liff 綁定頁
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Index()
        {
            IndexViewModel model = new IndexViewModel();

            try
            {
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }

            return View(model);
        }

        /// <summary>
        /// 商家Line帳號綁定SCM帳號 (LineBotUserID 綁定)
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult BindAccount([FromForm] IndexViewModel data)
        {
            ResponseModel<string> response = new ResponseModel<string>();

            try
            {
                response.Message = $"已綁定所屬商家：{data.CustomerNum}";
                response.Success = true;

                //設定跳轉商家資訊業面連結 (只能使用liff網址)
                //response.Data = "/liff/information";
                response.Data = "https://liff.line.me/1661441976-ZJrQ1aqK";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }

            return Json(response);
        }
        #endregion

        #region 商家資訊功能
        /// <summary>
        /// Line liff 商家資訊頁
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Information()
        {
            return View();
        }
        #endregion

        #region 資料下載功能
        /// <summary>
        /// Line liff 資料下載頁
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Download()
        {
            return View();
        }
        #endregion

        #region 資料上傳功能
        /// <summary>
        /// Line liff 資料上傳頁
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Upload()
        {
            return View();
        }
        #endregion

        #region 提問功能
        /// <summary>
        /// Line liff 提問頁
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Question()
        {
            QuestionViewModel model = new QuestionViewModel();

            try
            {
                //建立問題類別
                List<QuestionTypeModel> questionTypes = new List<QuestionTypeModel>();
                questionTypes.Add(new QuestionTypeModel { TypeID = 2, TypeName = "出貨", });
                questionTypes.Add(new QuestionTypeModel { TypeID = 1, TypeName = "訂單", });
                questionTypes.Add(new QuestionTypeModel { TypeID = 0, TypeName = "其他", });

                model.QuestionTypeSelectList = questionTypes;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }

            return View(model);
        }

        /// <summary>
        /// 提問送出
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult QuestionSend([FromForm] QuestionViewModel data)
        {
            ResponseModel response = new ResponseModel();

            try
            {
                response.Message = $"已收到您({data.LineBotUserID})的提問：{data.Message}";
                response.Success = true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }
            
            return Json(response);
        }
        #endregion

        #region 歷史問題功能
        /// <summary>
        /// Line liff 歷史問題列表頁面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult QuestionList(string userid = "")
        {
            QuestionListViewModel model = new QuestionListViewModel();

            if (!string.IsNullOrEmpty(userid))
            {
                model.LineBotUserID = userid;
            }

            return View(model);
        }

        /// <summary>
        /// 結案
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult FinishQuestion(string userId, string questionNo)
        {
            ResponseModel response = new ResponseModel();

            try
            {
                response.Message = $"您({userId})已將({questionNo}0結案";
                response.Success = true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }

            return Json(response);
        }

        /// <summary>
        /// Line liff 進入歷史問題聊天室
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult QuestionChatBox(string questionNo, string userId = "")
        {
            QuestionChatBoxViewModel model = new QuestionChatBoxViewModel();

            try
            {
                //設定UserID
                model.LineBotUserID = userId;
                //設定QuestionNo
                model.QuestionNo = questionNo;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }

            return View(model);
        }

        /// <summary>
        /// 聊天室 上傳檔案 Modal
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult UploadFileModal()
        {
            return PartialView("_questionDetailUploadModal");
        }

        /// <summary>
        /// 回覆聊天室
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult QuestionChatBoxReply([FromForm] QuestionChatBoxViewModel data)
        {
            ResponseModel response = new ResponseModel();

            try
            {
                response.Message = $"已收到您({data.LineBotUserID})的回覆：{data.Message}";
                response.Success = true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }

            return Json(response);
        }
        #endregion


        /// <summary>
        /// Line liff 綁定頁
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Index2()
        {

            try
            {
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }

            return View();
        }

    }
}
