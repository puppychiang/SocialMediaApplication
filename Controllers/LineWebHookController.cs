using isRock.LineBot;
using LineOfficial_MVC.Keys;
using LineOfficial_MVC.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Drawing;
using System.Net.Http.Headers;
using System.Text.RegularExpressions;
using System.Threading;

namespace LineOfficial_MVC.Controllers
{
    public class LineWebHookController : LineWebHookControllerBase
    {
        private readonly ILogger<LineWebHookController> _logger;
        /// <summary>
        /// AppSettings
        /// </summary>
        private readonly AppSettings _appSettings;
        /// <summary>
        /// line webhook use api uri (取得使用者傳送的檔案)
        /// </summary>
        private string _uri = "";
        /// <summary>
        /// 貼圖stickerId列表
        /// </summary>
        private readonly List<int> _stickerIds = new List<int>()
        {
            51626494, 51626495, 51626496, 51626497, 51626498
        };

        /// <summary>
        /// 建構子
        /// </summary>
        public LineWebHookController(ILogger<LineWebHookController> logger, AppSettings appSettings)
        {
            _logger = logger;
            _appSettings = appSettings;
        }


        #region Method
        /// <summary>
        /// Line Messaging API Webhook (發送訊息後，Line Bot主動觸發的API)
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("api/[controller]/[action]")]
        public async Task<ActionResult> Webhook()
        {
            // Line Messaging API Client (自行撰寫的功能)
            //LineMessagingApiClient _messagingClient = new LineMessagingApiClient();
            // 我的 Line@ Channel Token
            this.ChannelAccessToken = _appSettings.LineMessaging.channelAccessToken;
            // webhook Api Uri
            this._uri = _appSettings.LineMessaging.webhookApiUri;
            // 動作類型(message、join)
            string actionType = string.Empty;
            // 訊息類型(text、sticker、image、video、audio、location、file)
            string messageType = string.Empty;
            // 用戶在此 Line@ 聊天室的 UserID
            string userId = string.Empty;

            try
            {
                //取得Line Event
                var LineEvent = this.ReceivedMessage.events.FirstOrDefault();

                //判斷接收到的動作類型
                if (LineEvent != null)
                {
                    actionType = LineEvent.type;
                    if (actionType == "message")
                    {
                        #region 取得基礎資訊
                        //取得userId
                        userId = LineEvent.source.userId;
                        //取得使用者profile (名稱、大頭貼)
                        LineUserInfo lineUserInfo = this.GetUserInfo(userId);
                        //取得訊息類型
                        messageType = LineEvent.message.type;
                        #endregion

                        #region 根據用戶傳輸的訊息類型回傳對應的資訊
                        //回覆訊息
                        if (messageType == ReceiveMessageTypeEnum.Text)
                        {
                            // 等待秒數
                            int seconds = Int32.Parse(LineEvent.message.text);
                            //int seconds = 1000;
                            Thread.Sleep(seconds);
                            this.ReplyMessage(LineEvent.replyToken, $"如果你可以看到這則訊息代表等待了{seconds/1000}秒");

                            //使用圖文訊息回覆
                            //ButtonsTemplate buttonsTemplate_reply = new ButtonsTemplate();
                            //buttonsTemplate_reply.title = "";
                            //buttonsTemplate_reply.text = "抱歉，不支援於聊天室詢問問題";
                            //buttonsTemplate_reply.actions.Add(new UriAction
                            //{
                            //    label = "點我開始詢問",
                            //    uri = new Uri("https://liff.line.me/1661441976-R3dwolX5")
                            //});
                            //this.ReplyMessage(LineEvent.replyToken, buttonsTemplate_reply);
                        }
                        //回覆貼圖
                        else if (messageType == ReceiveMessageTypeEnum.Sticker)
                        {
                            Random rnd = new Random();
                            int randIndex = rnd.Next(_stickerIds.Count);
                            int random = _stickerIds[randIndex];
                            this.ReplyMessage(LineEvent.replyToken, 11538, random);
                        }
                        //回覆照片
                        else if (messageType == ReceiveMessageTypeEnum.Image)
                        {
                            //#region 讀取上傳的檔案內容
                            ////取得檔案id
                            //string imageId = LineEvent.message.id;
                            //// 由Line Messaging API取得檔案內容
                            //byte[] fileByteArray = await _messagingClient.GetContentBytesAsync(this.ChannelAccessToken, this._uri, imageId);
                            //// 解析檔案內容
                            //if (fileByteArray != null)
                            //{
                            //    using (MemoryStream ms = new MemoryStream(fileByteArray))
                            //    {
                            //        // Image use System.Drawing.Common nuget
                            //        using (Image image = Image.FromStream(ms))
                            //        {
                            //            this.ReplyMessage(LineEvent.replyToken, $"檔案大小：{image.Height}X{image.Width}");
                            //        }
                            //    }
                            //}
                            //#endregion

                            //this.ReplyMessage(LineEvent.replyToken, new Uri("https://admin.hotelnews.tw/FileUploads/hotel-1/logo.png"));
                        }
                        //回覆影片
                        else if (messageType == ReceiveMessageTypeEnum.Video)
                        {
                            this.ReplyMessage(LineEvent.replyToken, $"{lineUserInfo.displayName}傳了影片");
                        }
                        //回覆音訊
                        else if (messageType == ReceiveMessageTypeEnum.Audio)
                        {
                            this.ReplyMessage(LineEvent.replyToken, $"{lineUserInfo.displayName}傳了音訊");
                        }
                        //回覆地點
                        else if (messageType == ReceiveMessageTypeEnum.Location)
                        {
                            this.ReplyMessage(LineEvent.replyToken, $"{lineUserInfo.displayName}傳了地點");
                        }
                        //回覆收到的excel檔案內容
                        else if (messageType == ReceiveMessageTypeEnum.File)
                        {
                            //ServiceResponseModel<ImportOrderListViewModel> _result = new();

                            //#region 讀取上傳的檔案內容
                            ////取得檔案id
                            //string fileId = LineEvent.message.id;
                            ////取得檔案名稱
                            //string fileName = LineEvent.message.fileName;
                            ////檢查副檔名 (只允許 xlsx、xls)
                            //string _extension = Path.GetExtension(fileName); //取得副檔名
                            //if (_extension == ".xlsx" || _extension == ".xls")
                            //{
                            //    //var fileStream = await MessagingClient.GetContentStreamAsync(fileId);

                            //    // 由Line Messaging API取得檔案內容
                            //    ContentStream fileStream = await _messagingClient.GetContentStreamAsync(this.ChannelAccessToken, this._uri, fileId);
                            //    //解析檔案內容
                            //    if (fileStream != null)
                            //    {
                            //        _result = await _shipmentService.GetImportOrderExcelData(1, _extension, fileStream);
                            //    }
                            //}
                            //#endregion

                            //if (_result.Success)
                            //{
                            //    if (_result.Data.ShipmentDetailList.Count() > 0)
                            //    {
                            //        string replyText = $"{_result.Message}\n您的出貨明細：\n";
                            //        foreach (CustomerShipmentDetailViewModel orderItem in _result.Data.ShipmentDetailList)
                            //        {
                            //            replyText += $"訂單編號{orderItem.OrderNum},商品名稱{orderItem.ProductName},台幣總金額{orderItem.TWDTotalAmount}\n";
                            //        }

                            //        this.ReplyMessage(LineEvent.replyToken, replyText);
                            //    }
                            //}
                            //else
                            //{
                            //    this.ReplyMessage(LineEvent.replyToken, _result.Message);
                            //}
                        }
                        #endregion
                    }
                    else if (actionType == "join")
                    {
                        this.ReplyMessage(LineEvent.replyToken, "我已加入");
                    }
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
