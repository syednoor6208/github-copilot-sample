using Library.ApplicationCore.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Library.ApplicationCore.Services;

public class BookService
{
    public bool IsBookAvailable(IEnumerable<BookItem> bookItems)
    {
        return bookItems.Any(item => item.IsAvailable);
    }
}