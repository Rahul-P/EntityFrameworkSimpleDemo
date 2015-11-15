
namespace DiscussionBoard.DomainClasses
{
    using DiscussionBoard.DomainClasses.Enums;
    using System.Collections.Generic;

    public class Category
    {
        // Category will have a Collection of
        // "Topics".
        // So, the Topics table will have 
        // CategoryId in it as a column.
        // CategoryId will be the Foreign Key
        // in Topics Table.
        // One Category can have oneo-to-many*
        // Topics. Hence we have a collection of
        // Topics in Category Class.
        private ICollection<Topic> _topics;

        // Overriding Default Constructor
        public Category()
        {
            _topics = new List<Topic>();
        }

        // This will be the Primary Key
        public int CategoryId { get; set; }

        // Name of the Category 
        // Example - Science/Sports etc       
        public DiscussionCategories Name { get; set; }

        // The one or many Topic(s) that a category
        // can have.
        // Collection of those Topics.
        public virtual ICollection<Topic> Topics
        {
            get { return _topics; }
            set { _topics = value; }
        }
    }
}
