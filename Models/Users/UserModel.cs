namespace WebApi.Models.Users
{
  public class UserModel
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public bool IsLocked { get; set; }
        public int StoreID { get; set; }
        public int ApplicationID { get; set; }
    }
}