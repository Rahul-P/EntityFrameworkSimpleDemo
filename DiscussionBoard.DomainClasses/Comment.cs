
namespace DiscussionBoard.DomainClasses
{
    using System;

    public class Comment
    {
        // Now we have reached the end
        // of the tail.
        // Comment will have no collection
        // of any other class.
        // Comment will have a foreign key
        // which will be MemberId.
        // MemberId will be the primary key
        // of Member Table.
        
        // This will be the Primary Key
        public int CommentId { get; set; }

        // This will store the Comment made
        // Example - I have no clue why 
        // "Elizabeth Warren" is not running 
        // for Presidency! She would be a 
        // delight to vote for! 
        public string CommentDetail { get; set;}

        // Column Added Post Database Creation
        // The Likes Counter
        public int NumberOfLikes { get; set; }        

        // MemberId from Member Table
        // This is a Foreign Key in Comment Table
        public int MemberId { get; set; }      

        // Comment Created Date Time
        public DateTime CreatedDateTime { get; set; }

        // The Topic this Comment is related too
        public int TopicId { get; set; }

        // Navigation Property to Topic
        public virtual Topic Topic { get; set; }
    }
}
