using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Manageras;
using Data.Repositories.DBRepositories;
using Models;
using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace Tests
{
    [TestFixture]
    public class DbTagRepoTests
    {
        TagManager _tag = new TagManager();
        [Test]
        public void CanListAllTags()
        {
            Assert.AreEqual(3, _tag.ListTags().Data.Count);
        }

        [Test]
        public void CanGetTagById()
        {
            Assert.AreEqual("French", _tag.GetTag(3).Data.TagTitle);
        }

        [Test]
        public void CanAddTagToDb()
        {
            Tag tag = new Tag();
            tag.TagTitle = "European";
            _tag.AddTag(tag);
            Assert.AreEqual(4, _tag.ListTags().Data.Count);
        }

        [Test]
        public void CanEditTag()
        {
            var tagged = _tag.GetTag(5).Data;
            tagged.TagTitle = "Chinese";
            _tag.EditTag(tagged);
            Assert.AreEqual("Chinese", _tag.GetTag(5).Data.TagTitle);
        }

        [Test]
        public void CanDeleteTagFromDb()
        {
            _tag.RemoveTag(1);
            Assert.AreEqual(3 , _tag.ListTags().Data.Count);
        }

    }
}
