
namespace DiscussionBoard.DomainClasses
{
    using System;
    using System.Collections.Generic;

    public class Member
    {
        // A Member will have a collection of Interests - Like Science, 
        // Technology, Sports, Politics among many others that they
        // would like to discuss.
        private ICollection<Interest> _interests;

        // Overriding Default Constructor
        public Member()
        {
          _interests = new List<Interest>();     
        }

        // This will be the Primary Key
        public int MemberId { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }

        // A Member will have Contact Detail
        // One-to-One* releationship
        // Navigation Property to Contact Detail
        public virtual ContactDetail ContactDetail { get; set; }
    }
}
