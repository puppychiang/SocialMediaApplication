namespace LineOfficial_MVC.Keys
{
    /// <summary>
    /// Receive Line Message Type (接收用)
    /// </summary>
    public class ReceiveMessageTypeEnum
    {
        public const string Text = "text";
        public const string Sticker = "sticker";
        public const string Image = "image";
        public const string Video = "video";
        public const string Audio = "audio";
        public const string Location = "location";
        public const string File = "file";
    }

    /// <summary>
    /// Reply Line Message Type (回覆用)
    /// </summary>
    public class ReplyMessageTypeEnum
    {
        public const string Text = "text";
        public const string Sticker = "sticker";
        public const string Image = "image";
        public const string Video = "video";
        public const string Audio = "audio";
        public const string Location = "location";
        public const string Imagemap = "imagemap";
        public const string Template = "template";
        public const string Flex = "flex";
    }
}
