using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Manageras;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class DbPostRepoTests
    {
        PostingManager _post = new PostingManager();

        [Test]
        public void CanListAllPostings()
        {
            Assert.AreEqual(1, _post.ListPostings().Data.Count);
        }

        [Test]
        public void CanGetPostById()
        {
            //Assert.AreEqual("Longhorn Review", _post.GetPost(7).Data.Title);
            Assert.AreEqual(7, _post.GetPost(7).Data.PostId);
        }

        [Test]
        public void CanAddPostToDb()
        {
            var post = _post.GetPost(7).Data;
            post.Title = "PostPost";
            post.DeleteOn = DateTime.Now;
            _post.AddPost(post);
            Assert.AreEqual(2, _post.ListPostings().Data.Count);
        }

        [Test]
        public void CanEditPostRecordFromDb()
        {
            var post = _post.GetPost(10).Data;
            post.DeleteOn = DateTime.Now;
            post.Title = "Edit from Test v1";
            _post.EditPost(post);
            Assert.AreEqual("Edit from Test v1", _post.GetPost(10).Data.Title);
        }

        [Test]
        public void CanDeletePostFromDb()
        {
            _post.RemovePost(7);
            Assert.AreEqual(4, _post.ListPostings().Data.Count);
        }
    }
}
