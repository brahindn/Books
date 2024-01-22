using System.Collections;

namespace Books_New.Console
{
    public class FilterConditions// : IEnumerable
    {
        public List<string> BookName {  get; set; }
        public List<string> AuthorName { get; set; }
        public List<string> GenreName { get; set; }
        public List<string> PublisherName { get; set; }
        public List<string> PageNumber { get; set; }
        public List<string> RealiseDate { get; set; }

        public IEnumerator GetEnumerator()
        {
            if(BookName != null)
            {
                foreach(var book in BookName)
                {
                    yield return book;
                }
            }
            if(AuthorName != null)
            {
                foreach(var author in AuthorName)
                {
                    yield return author;
                }
            }
            if(GenreName != null)
            {
                foreach (var genre in GenreName)
                {
                    yield return genre;
                }
            }
            if(PublisherName != null)
            {
                foreach(var publisher in PublisherName)
                {
                    yield return publisher;
                }
            }
            if(PageNumber != null)
            {
                foreach(var page in PageNumber)
                {
                    yield return page;
                }
            }
            if(RealiseDate != null)
            {
                foreach( var realiseDate in RealiseDate)
                {
                    yield return realiseDate;
                }
            }
        }
    }
}
