

namespace DiscussionBoard.DomainClasses
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    public class ContactDetail
    {
        // This Class has No Explicit Primary Key Declared              

        // Mobile Number - if dicussion have to be moved away from 
        // the online podium - to organise hate-filled face-to-face 
        // cath-ups etc
        public string MobilePhone { get; set; }

        // For the Brave hearts who want to be identified
        // using Twitter details
        public string TwitterAlias { get; set; }

        // For the Brave hearts who want to be identified
        // using Facebook details
        public string Facebook { get; set; }

        // For Professional People.
        // Pretending to keep it classy?
        // Then this is for you.
        public string LinkedIn { get; set; }

        // The MemberId this detail belongs too
        [Key, ForeignKey("Member")]
        public int MemberId { get; set; }

        // Navigation Property to Member
        public virtual Member Member { get; set; }
    }
}
