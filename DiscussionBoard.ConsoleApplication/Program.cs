
namespace DiscussionBoard.ConsoleApplication
{
    using DiscussionBoard.DataLayer;
    using DiscussionBoard.DomainClasses;
    using DiscussionBoard.DomainClasses.Enums;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;

    public class Program
    {
        static void Main(string[] args)
        {           
            //GetMembers();
            
            // UNCOMMENT This when you have done reading about SetInitializer 
            // section in the ARTICLE.
            // Disabling Database Initialization
            Database.SetInitializer(new NullDatabaseInitializer
               <DiscussionBoardContext>());

            // Initial Inserts

            InsertCatergories();

            if (!TopicExists())
                InsertTopics();

            if (!MembersExists())
                InsertMembers();

            if (!InterestsExists())
                InsertInterests();

            // Stand Alone Insert Contact Details will not be required.
            // As we insert this when we insert Member Records.        
            // InsertContactDetails();

            if (!CommentsExists())
                InsertComments();


            // UNCOMMENT the code below once you get to the 
            // section: Interacting with Database

            // Update Example Methods
            //UpdateRandomComment();
            //UpdateRandomCommentInDisconnectedState();


            // UNCOMMENT the code below once you get to the 
            // section: Interacting with Database 
            // - Deleting Records in Discussion Database
            //DeleteRandomComment();


            // UNCOMMENT the code below once you get to the 
            // section: Interacting with Database 
            // - Retrieving Graphs of Data Records from Discussion Database
            RetrievingDataGraphs(); 

        }

        private static void RetrievingDataGraphs()
        {
            // Fetch a Topic Record and all related Comments to that Topic

            Topic eagerLoadedTopic;            
            using (var context = new DiscussionBoardContext())
            {
                // Example of Eager Loading
                eagerLoadedTopic = context.Topics.Where(t => t.TopicName
                    .Contains("Will Mr.Gates ever"))
                    .Include(t => t.Comments)
                    .FirstOrDefault();
            }

            Topic explicitLoadedTopic;
            using (var context = new DiscussionBoardContext())
            {
                // Example of Explicit Loading
                explicitLoadedTopic = context.Topics.Where(t => t.TopicName
                    .Contains("Will Mr.Gates ever"))              
                    .FirstOrDefault();

                // Explicit Loading done here
                context.Entry(explicitLoadedTopic)
                       .Collection(t => t.Comments)
                       .Load();
            }
        }

        private static void DeleteRandomComment()
        {
            // Fetching a Random Comment
            Comment comment;
            using (var context = new DiscussionBoardContext())
            {
                // Get me a random comment
                comment = context.Comments.FirstOrDefault();
            }            

            using (var context = new DiscussionBoardContext())
            {
                context.Database.Log = Console.WriteLine;

                // Note* 
                // We need to apprise this Context of the changes we would 
                // like to be made in the Database to the 
                // fetched Comment Object!

                // Marking the object as Deleted for the Context to
                // take action and fire a Delete Statement for the relevant
                // Comment Object.
                context.Entry(comment).State = EntityState.Deleted;

                // Save the Updated Comment Object
                context.SaveChanges();
                Console.ReadKey();
            }
        }

        private static void UpdateRandomComment()
        {
            // Fetching a Random Comment
            Comment comment;
            using (var context = new DiscussionBoardContext())
            {
                context.Database.Log = Console.WriteLine;

                // Get me a random comment
                comment = context.Comments.FirstOrDefault();

                // Lets make some changes
                comment.CommentDetail = "Comment Updated by: UpdateRandomComment()" +
                    " Method on DateTime" + System.DateTime.Now.ToString();

                // Save the Updated Comment Object
                context.SaveChanges();
                Console.ReadKey();
            }            
        }

        private static void UpdateRandomCommentInDisconnectedState()
        {
            // Fetching a Random Comment
            Comment comment;
            using (var context = new DiscussionBoardContext())
            {
                // Get me a random comment
                comment = context.Comments.FirstOrDefault();                
            }

            // Update the data now
            // This will mimick us fetching data and passing
            // it to some sort of client - example a Web page 
            // for update.

            // Mimicking Update the Web Client will do
            comment.CommentDetail = "Comment Updated by: "+
                "UpdateRandomCommentInDisconnectedState()" +
                    " Method on DateTime" + System.DateTime.Now.ToString();

            // Now, the data HashSet to TransactionalBehavior saved.

            using (var context = new DiscussionBoardContext())
            {
                context.Database.Log = Console.WriteLine;      
       
                // Note* This Context has NO IDEA of what has happened to the 
                // above Comment Object - It has no Knowledge that it has been
                // Updated!

                // We need to apprise it of the changes we have made to the 
                // Comment Object!

                // Marking the object as Updated/Modified for the Context to
                // take action and fire a Update Statement for the relevant
                // Comment Object.
                context.Entry(comment).State = EntityState.Modified;

                // Save the Updated Comment Object
                context.SaveChanges();
                Console.ReadKey();
            }            
        }

        private static void InsertComments()
        {
            IList<Comment> comments = new List<Comment>();
            // Fetching Members...
            IList<Member> members;
            using (var context = new DiscussionBoardContext())
            {
                members = context.Members
                                    .Where(m => m.FirstName
                                        .Contains("Member")).ToList();
            }
            //Fetching Topic
            Topic topic;
            using (var context = new DiscussionBoardContext())
            {
                topic = context.Topics.Where(t => t.TopicName
                    .Contains("Will Mr.Gates ever")).FirstOrDefault();
            }
            // Time to Create Comments for Members      
            foreach (var member in members)
            {
                var comment = new Comment
                {                
                    CommentDetail = "The Comment is:" + 
                         " from: " + member.FirstName + " " + member.LastName,

                    // Use Foreign Key to indicate this is not a Insert!
                    MemberId = member.MemberId,
                    // Use Foreign Key to indicate this is not a Insert!
                    TopicId = topic.TopicId,

                    CreatedDateTime = System.DateTime.Now,
                    NumberOfLikes = 0
                };            
                comments.Add(comment);
            }
            // Saving New Comments
            using (var context = new DiscussionBoardContext())
            {
                context.Database.Log = Console.WriteLine;

                // Everything in the Graph will be marked as Added Now!        
                context.Comments.AddRange(comments);

                context.SaveChanges();
                Console.ReadKey();
            }
        }    

        private static void InsertInterests()
        {
            IList<Interest> interests = new List<Interest>();
            // Fetching Members...
            IList<Member> members;
            using (var context = new DiscussionBoardContext())
            {
                members = context.Members
                                    .Where(m => m.FirstName
                                        .Contains("Member")).ToList();                           
            }
            // Time to Create Interests for Members
            int enumCounter = 1;
            foreach(var member in members)
            {                
                var interest = new Interest
                {
                    // the power of Enums! 
                    InterestName = (DiscussionCategories) enumCounter,
                    // Use Foreign Key to indicate this is not a Insert!
                    MemberId = member.MemberId
                };
                enumCounter++;
                interests.Add(interest);
            }
            // Saving New Interests
            using (var context = new DiscussionBoardContext())
            {
                context.Database.Log = Console.WriteLine;

                // Everything in the Graph will be marked as Added Now!        
                context.Interests.AddRange(interests);

                context.SaveChanges();
                Console.ReadKey();
            }
        }        

        private static void InsertMembers()
        {
            IList<Member> members = new List<Member>();
            for (int i=1; i<= 5; i++)
            {
                var member = new Member
                {
                    FirstName = "Member" + i,
                    LastName = "SomeLastName" + i,

                    // BOTS have just been born.. RUN...
                    DateOfBirth = System.DateTime.Now,

                    // Adding ContactDetaiul record on the FLY!...
                    ContactDetail = new ContactDetail
                    {
                        MobilePhone = "MobileNumber" + i,
                        TwitterAlias = "TwitterAlias" + i,
                        Facebook = "Facebook" + i,
                        LinkedIn = "Linkedin" + i
                    }                    
                };
                members.Add(member);
            }
            // Saving New Members
            using (var context = new DiscussionBoardContext())
            {
                context.Database.Log = Console.WriteLine;

                // Everything in the Graph will be marked as Added Now!
                // Including the ContactDetail Records...
                context.Members.AddRange(members);             

                context.SaveChanges();
                Console.ReadKey();
            }
        }

        private static Boolean TopicExists()
        {
            Topic topic;
            using (var context = new DiscussionBoardContext())
            {
                topic = context.Topics.Where(t => t.TopicName
                    .Contains("Will Mr.Gates ever")).FirstOrDefault();
            }
            return (topic != null);
        }

        private static Boolean MembersExists()
        {
            IList<Member> members;
            using (var context = new DiscussionBoardContext())
            {
                members = context.Members.Where(t => t.FirstName
                    .Contains("Member")).ToList();
            }
            return (members.Count > 0);
        }

        private static Boolean InterestsExists()
        {
            IList<Interest> interests;
            using (var context = new DiscussionBoardContext())
            {
                interests = context.Interests.Where(i =>
                        i.InterestName == DiscussionCategories.Science
                        || i.InterestName == DiscussionCategories.Sports
                        || i.InterestName == DiscussionCategories.Technology
                        || i.InterestName == DiscussionCategories.Politics
                        || i.InterestName == DiscussionCategories.Philosophy
                        ).ToList();
            }
            return (interests.Count > 0);
        }

        private static Boolean CommentsExists()
        {
            IList<Comment> comments;
            using (var context = new DiscussionBoardContext())
            {
                comments = context.Comments.Where(c => c.CommentDetail
                    .Contains("The Comment is:")).ToList();
            }
            return (comments.Count > 0);
        }

        private static void InsertTopics()
        {
            // Fetching Categories First: Technology and Sports.
            IList<Category> categories;
            using (var context = new DiscussionBoardContext())
            {
                categories = context.Categories
                                    .Where(c => 
                                        c.Name == DiscussionCategories.Technology
                                        || c.Name == DiscussionCategories.Sports)
                                    .ToList();
            }
            // Time to Create a Topic which will fall under The above 2 Categories
            Topic topic = new Topic
            {
                StartDate = System.DateTime.Now,
                IsActive = true,
                TopicName = "Will Mr.Gates ever buy a Basketball Team?"
            };
            foreach (var _category in categories)
            {
                var categoryRec = new Category
                {
                    CategoryId = _category.CategoryId,
                    Name = _category.Name
                };                                     
                topic.Categories.Add(categoryRec);
            }            
            // Saving New Topic
            using (var context = new DiscussionBoardContext())
            {
                context.Database.Log = Console.WriteLine;                

                // Everything in the Graph will be marked as Added Now!
                // Including the Category Records...
                context.Topics.Add(topic);

                // Marking Category Records as UnChanged - No Insert required!
                foreach (var entry in context.ChangeTracker.Entries<Category>())
                {
                    entry.State = EntityState.Unchanged;
                }               

                context.SaveChanges();
                Console.ReadKey();
            }
        }

        private static IList<Category> _getInitialCategories()
        {
            // List of Categories to Insert
            return new List<Category>()
            {
                new Category()
                {
                    Name = DiscussionCategories.Science
                },
                new Category()
                {
                    Name = DiscussionCategories.Sports
                },
                new Category()
                {
                    Name = DiscussionCategories.Technology
                },
                new Category()
                {
                    Name = DiscussionCategories.Politics
                },
                new Category()
                {
                    Name = DiscussionCategories.Philosophy
                }
            };
        }

        private static void InsertCatergories()
        {            
            using (var context = new DiscussionBoardContext())
            {
                IList<Category> _categories;
                _categories = context
                    .Categories
                    .Where(c =>
                           c.Name == DiscussionCategories.Science
                        || c.Name == DiscussionCategories.Sports
                        || c.Name == DiscussionCategories.Technology
                        || c.Name == DiscussionCategories.Politics
                        || c.Name == DiscussionCategories.Philosophy
                        ).ToList();

                if (_categories.Count > 0)
                {
                    // Do Nothing.
                }
                else
                {
                    // No Records Found - Add Them.
                    IList<Category> categories = _getInitialCategories();
                    foreach (var definedCategory in categories)
                    {
                        context.Database.Log = Console.WriteLine;
                        context.Categories.Add(definedCategory);
                        context.SaveChanges();

                        // To read the Log File in Console in Detail
                        Console.ReadKey();
                    }
                }
            }
        }

        private static void GetMembers()
        {
            // Instantiate our Discussion Board Context
            using (var context = new DiscussionBoardContext())
            {
                // Some Notes -
                // 1.
                // Make sure Refernce to DiscussionBoard.DomainClasses Project 
                // has been added to this Console Application Project.
                // 2. 
                // Execute a simple query to return all Members.
                // 3. 
                // ToList() is a LINQ mehtod and also an Executing Method.
                // So the ToList() method will actually trigger the 
                // query execution in this case! to return all the Members.
                var members = context.Members.ToList();

                foreach(var singleMember in members)
                {
                    // Print the First Name on Console.
                    Console.WriteLine(singleMember.FirstName);
                }
            }            
        }
    }
}
