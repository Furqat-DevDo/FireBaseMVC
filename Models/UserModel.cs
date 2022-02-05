namespace FireBaseMVC.Models;
public class UserModel
{
   public Guid ID { get; set; } = Guid.NewGuid();
   
   
   public string FullName { get; set; }
   
   public string Password { get; set; }
   
   public string Email { get; set; }
   
   public DateTimeOffset  BirthDate { get; set; }
   
}