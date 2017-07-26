using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Interfaces;
using Models.Postings;
using Models.Restaurants;

namespace Data.Repositories
{
    public class PostingRepo : IPost
    {
        private static List<Post> _post;

        public PostingRepo()
        {
            _post = new List<Post>()
            {
                new Post()
                {
                    PostId = 1,
                    ReviewText = "This place was pretty good",
                    Title = "I tried Zub's and this is my reaction",
                    Restaurant = new Restaurant()
                    {
                        Name = "Mr. Zub's Deli"
                    }
                },
                new Post()
                {
                    PostId = 2,
                    ReviewText = "This place was pretty good",
                    Title = "I had Nuevo and it was the bomb.com",
                    Restaurant = new Restaurant()
                    {
                        Name = "Nuevo"
                    }
                }
            };

        }
        public List<Post> ListPostings()
        {
            return _post;
        }

        public Post GetPost(int id)
        {
            return _post.FirstOrDefault(p => p.PostId == id);
        }

        public void AddPost(Post post)
        {
            _post.Add(post);
        }

        public void EditPost(Post post)
        {
            //this needs to be better planned in regards to the database
            _post.RemoveAll(p => p.PostId == post.PostId);
            _post.Add(post);
        }

        public void RemovePost(int id)
        {
            _post.RemoveAll(p => p.PostId == id);
        }
    }
}
