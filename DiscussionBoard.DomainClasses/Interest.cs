
namespace DiscussionBoard.DomainClasses
{
    using DiscussionBoard.DomainClasses.Enums;

    public class Interest
    {
        // This will be the Primary Key
        public int InterestId { get; set; }

        // Name of the Interest 
        // Example - Science/Sports etc      
        public DiscussionCategories InterestName { get; set; }

        // MemberId of the Member this Interest belongs too
        public int MemberId { get; set; }

        // Navigation Property to Member
        public virtual Member Member { get; set; }
    }
}
