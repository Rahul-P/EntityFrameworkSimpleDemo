
namespace DiscussionBoard.DomainClasses
{
    using System;
    using System.Collections.Generic;

    // Note* A Topic may well fall under more than 
    // 1 Category. So, a Topic will also have a
    // collection of Categories.
    public class Topic
    {
        // Topic will have a Collection of
        // "Comments".
        // So, the Comment table will have 
        // TopicId in it as a column.
        // TopicId will be the Foreign Key
        // in Comment Table.
        // One Topic can have oneo-to-many*
        // Comments. Hence we have a collection of
        // Comments in Topic Class.
        private ICollection<Comment> _comments;
        private ICollection<Category> _categories;

        // Overriding Default Constructor
        public Topic()
        {
            _comments = new List<Comment>();
            _categories = new List<Category>();
        }

        // This will be the Primary Key
        public int TopicId { get; set; }

        // Topic Start Date Time
        public DateTime StartDate { get; set; }

        // This flag represents that if the Topic
        // is still active or closed for discussion
        public bool IsActive { get; set; }

        // Name of the Topic 
        // Example - Why is "Elizabeth Warren" not 
        // contesting in 2016 elections?
        public string TopicName { get; set; }

        // The one or many Comments(s) that a topic
        // can have.
        // Collection of those Comments.
        public virtual ICollection<Comment> Comments
        {
            get { return _comments; }
            set { _comments = value; }
        }

        // The one or many Categories(s) that a topic
        // can fall under.
        // Collection of those Categories.
        public virtual ICollection<Category> Categories
        {
            get { return _categories; }
            set { _categories = value; }
        }        
    }
}
