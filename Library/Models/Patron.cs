using System.Collections.Generic;

namespace Library.Models
{
  public class Patron
  {
    public Patron()
    {
      this.JoinEntities = new HashSet<Checkout>();
    }
    public int PatronId { get; set; }
    public string PatronName { get; set; }
    public string PatronPhoneNumber { get; set; }
    public virtual ApplicationUser User { get; set; }
    public virtual ICollection<Checkout> JoinEntities { get; set; }
  }
}