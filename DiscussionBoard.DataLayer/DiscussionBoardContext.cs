
namespace DiscussionBoard.DataLayer
{
    using DiscussionBoard.DomainClasses;
    using System.Data.Entity;

    // The Default Database Name that Code First will be
    // looking for will be fully qualified name of our DbContext Class...
    // In our case that would be the following -
    // Database Name: "DiscussionBoard.DataLayer.DiscussionBoardContext"
    public class DiscussionBoardContext: DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Comment> Comments { get; set; }        
        public DbSet<Interest> Interests { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<Topic> Topics { get; set; }

        public DiscussionBoardContext()
        {
            // Diable LazyLoading!
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;
        }
    }
}
