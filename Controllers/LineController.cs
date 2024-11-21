using isRock.LineBot;
using LineOfficial_MVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace LineOfficial_MVC.Controllers
{
    public class LineController : LineWebHookControllerBase
    {
        private readonly ILogger<LineController> _logger;
        /// <summary>
        /// AppSettings
        /// </summary>
        private readonly AppSettings _appSettings;

        /// <summary>
        /// 建構子
        /// </summary>
        public LineController(ILogger<LineController> logger, AppSettings appSettings)
        {
            _logger = logger;
            _appSettings = appSettings;

            // 我的 Line@ Channel Token
            this.ChannelAccessToken = _appSettings.LineMessaging.channelAccessToken;
        }

        #region Template Message Buttons (按鈕連結) 發送
        /// <summary>
        /// 主動發送 Line Template Message Buttons 訊息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("[controller]/[action]/{userId?}")]
        public async Task<ActionResult> SendButtons(string userId)
        {
            try
            {
                if (!string.IsNullOrEmpty(userId))
                {
                    //好農主動發送的 圖文訊息 (Template message)
                    ButtonsTemplate buttonsTemplate_Admin = new ButtonsTemplate();
                    buttonsTemplate_Admin.title = "好農發送訊息給您";
                    buttonsTemplate_Admin.text = "請查收出貨明細，點選附件連結進行下載";
                    buttonsTemplate_Admin.actions.Add(new UriAction
                    {
                        label = "附件請點此處",
                        uri = new Uri("https://liff.line.me/1661441976-vnGLKdyP?questionNo=85a08227-35c2-85a4-2e70-f3d8e2206d93")
                    });
                    this.PushMessage(userId, buttonsTemplate_Admin);
                }

                return Ok();
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(userId))
                {
                    //主動推送訊息
                    this.PushMessage(userId, "發生錯誤:\n" + ex.Message);
                }
                return Ok();
            }
        }

        /// <summary>
        /// 好農回覆商家提問的 Line Template Message Buttons 訊息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("[controller]/[action]/{userId?}")]
        public async Task<ActionResult> ReplyButtons(string userId)
        {
            try
            {
                if (!string.IsNullOrEmpty(userId))
                {
                    //好農回覆商家提問的 圖文訊息 (Template message)
                    ButtonsTemplate buttonsTemplate_Reply = new ButtonsTemplate();
                    buttonsTemplate_Reply.title = "問題回覆";
                    buttonsTemplate_Reply.text = "提問的問題：\\n要如何進行註冊？\\n\\n回覆：\\n請點選註冊";
                    buttonsTemplate_Reply.actions.Add(new UriAction
                    {
                        label = "查看問題詳細訊息請點此處",
                        uri = new Uri("https://liff.line.me/1661441976-vnGLKdyP?questionNo=85a08227-35c2-85a4-2e70-f3d8e2206d93")
                    });
                    this.PushMessage(userId, buttonsTemplate_Reply);
                }

                return Ok();
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(userId))
                {
                    //主動推送訊息
                    this.PushMessage(userId, "發生錯誤:\n" + ex.Message);
                }
                return Ok();
            }
        }

        /// <summary>
        /// 轉發商家提問給所有所屬商家帳號的 Line Template Message Buttons 訊息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("[controller]/[action]/{userId?}")]
        public async Task<ActionResult> ForwardButtons(string userId)
        {
            try
            {
                if (!string.IsNullOrEmpty(userId))
                {
                    //轉發商家提問給所有所屬商家帳號的 圖文訊息 (Template message)
                    ButtonsTemplate buttonsTemplate_Customer = new ButtonsTemplate();
                    buttonsTemplate_Customer.title = "問題提問";
                    buttonsTemplate_Customer.text = "George提問的問題，好農已收到，會盡速的回覆。\\n\\n問題類型：註冊問題\\n問題描述：要如何進行註冊？";
                    buttonsTemplate_Customer.actions.Add(new UriAction
                    {
                        label = "查看問題詳細訊息請點此處",
                        uri = new Uri("https://liff.line.me/1661441976-vnGLKdyP?questionNo=85a08227-35c2-85a4-2e70-f3d8e2206d93")
                    });
                    this.PushMessage(userId, buttonsTemplate_Customer);
                }

                return Ok();
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(userId))
                {
                    //主動推送訊息
                    this.PushMessage(userId, "發生錯誤:\n" + ex.Message);
                }
                return Ok();
            }
        }
        #endregion


        #region Template Message Carousel (輪播) 發送
        /// <summary>
        /// 主動發送 Line Template Message Buttons 訊息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("[controller]/[action]/{userId?}")]
        public async Task<ActionResult> SendCarousel(string userId)
        {
            try
            {
                if (!string.IsNullOrEmpty(userId))
                {
                    CarouselTemplate carouselTemplate = new CarouselTemplate();
                    List<Column> columns = new List<Column>();

                    // 第一張輪播
                    Column column1 = new Column();
                    column1.title = "輪播1 標題";
                    column1.text = "輪播1 內文";
                    column1.thumbnailImageUrl = new Uri("https://admin.hotelnews.tw/FileUploads/hotel-1/2021-08-18/210818173837170-XXL.jpg");
                    column1.actions.Add(new UriAction
                    {
                        label = "連結",
                        uri = new Uri("https://liff.line.me/1661441976-vnGLKdyP?questionNo=85a08227-35c2-85a4-2e70-f3d8e2206d93")
                    });
                    columns.Add(column1);

                    // 第二張輪播
                    Column column2 = new Column();
                    column2.title = "輪播2 標題";
                    column2.text = "輪播2 內文";
                    column2.thumbnailImageUrl = new Uri("https://admin.hotelnews.tw/FileUploads/hotel-1/2021-08-18/210818173834138-XXL.jpg");
                    column2.actions.Add(new UriAction
                    {
                        label = "連結",
                        uri = new Uri("https://liff.line.me/1661441976-vnGLKdyP?questionNo=85a08227-35c2-85a4-2e70-f3d8e2206d93")
                    });
                    columns.Add(column2);

                    carouselTemplate.columns = columns;

                    this.PushMessage(userId, carouselTemplate);
                }

                return Ok();
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(userId))
                {
                    //主動推送訊息
                    this.PushMessage(userId, "發生錯誤:\n" + ex.Message);
                }
                return Ok();
            }
        }
        #endregion


        #region Template Message Confirm (確認) 發送
        /// <summary>
        /// 主動發送 Line Template Message Confirm 訊息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("[controller]/[action]/{userId?}")]
        public async Task<ActionResult> SendConfirm(string userId)
        {
            try
            {
                if (!string.IsNullOrEmpty(userId))
                {
                    ConfirmTemplate confirmTemplate = new ConfirmTemplate();
                    confirmTemplate.text = "幫忙按下Yes or No";
                    confirmTemplate.actions.Add(new MessageAction
                    {
                        label = "Yes",
                        text = "yes"
                    });
                    confirmTemplate.actions.Add(new MessageAction
                    {
                        label = "No",
                        text = "no"
                    });

                    this.PushMessage(userId, confirmTemplate);
                }

                return Ok();
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(userId))
                {
                    //主動推送訊息
                    this.PushMessage(userId, "發生錯誤:\n" + ex.Message);
                }
                return Ok();
            }
        }
        #endregion


    }
}
