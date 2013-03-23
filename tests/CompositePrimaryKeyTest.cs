using NUnit.Framework;

namespace SQLite.Tests
{
    using System;
    using System.Collections.Generic;

    [TestFixture]
    public class CompositePrimaryKeyTest
    {
        TestDb _db;

        public class Album
        {
            [PrimaryKey]
            public string Artist { get; set; }

            [PrimaryKey]
            public string Title { get; set; }

            public TimeSpan Duration { get; set; }
        }

        [SetUp]
        public void SetUp()
        {
            _db = new TestDb();
            _db.CreateTable<Album>();
        }

        [Test]
        public void DublicatePrimaryKey_ThrowsException()
        {
            _db.Insert(new Album() { Artist = "a", Title = "a", Duration = TimeSpan.FromMinutes(1) });
            
            Assert.Throws<SQLiteException>(() => _db.Insert(new Album() { Artist = "a", Title = "a", Duration = TimeSpan.FromMinutes(1) }));
        }

        [Test]
        public void FindByCompositeKey()
        {
            var a = new Album() { Artist = "a", Title = "a", Duration = TimeSpan.FromMinutes(1) };
            _db.Insert(a);

            var dbAlbum = _db.Find<Album>("a", "a");

            Assert.AreEqual(a.Duration, dbAlbum.Duration);
        }

        [Test]
        public void DeleteByObjectWithCompositeKey()
        {
            var a = new Album() { Artist = "a", Title = "a", Duration = TimeSpan.FromMinutes(1) };
            _db.Insert(a);
            _db.Delete(a);

            var dbAlbum = _db.Find<Album>("a", "a");

            Assert.IsNull(dbAlbum);
        }

        [Test]
        public void DeleteByCompositeKey()
        {
            var a = new Album() { Artist = "a", Title = "a", Duration = TimeSpan.FromMinutes(1) };
            _db.Insert(a);

            _db.Delete<Album>("a", "a");

            var dbAlbum = _db.Find<Album>("a", "a");

            Assert.IsNull(dbAlbum);
        }

        [Test]
        public void UpdateObjectWithCompositeKey()
        {
            var a = new Album() { Artist = "a", Title = "a", Duration = TimeSpan.FromMinutes(1) };
            _db.Insert(a);

            a.Duration = TimeSpan.FromHours(1);
            _db.Update(a);

            var dbAlbum = _db.Find<Album>("a", "a");

            Assert.AreEqual(a.Duration, dbAlbum.Duration);
        }
    }
}
