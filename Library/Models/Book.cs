using System.Collections.Generic;

namespace Library.Models
{
  public class Book
  {
    public Book()
    {
      this.JoinEntities = new HashSet<Checkout>();
    }
    public int BookId { get; set; }
    public string BookTitle { get; set; }
    public DateTime BookDueDate { get; set; }
    public virtual ICollection<Checkout> JoinEntities { get; }
  }
}