namespace MoodBot.Models.Db
{
    public class LastBotMessage
    {
        public int Id { get; set; }
        public string MessageCode { get; set; }
        public int UserId { get; set; }
    }
}
