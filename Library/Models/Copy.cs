using System.Collections.Generic;

namespace Library.Models
{
  public class Copy
  {
    public Copy()
    {
      this.JoinEntities = new HashSet<Checkout>();
    }
    public int CopyId { get; set; }
    public int BookId { get; set; }
    public bool CheckoutStatus { get; set; }
    public virtual ICollection<Checkout> JoinEntities { get; }
  }
}