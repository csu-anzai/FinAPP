using System;
using System.Collections.Generic;
using System.Text;
using BLL.Security;
using Moq;
using NUnit.Framework;

namespace BLL.Tests.SecurityTests
{
    class PassHasherTest
    {
        private IPassHasher _passHasher;
        private string password = "true password";
        private string fakePassword = "fake password";

        [SetUp]
        public void SetUp()
        {
            _passHasher = new PassHasher();
        }

        [Test]
        public void HashPassword_ReturnsHashedPassword()
        {
            var hash = _passHasher.HashPassword(password);

            Assert.False(string.IsNullOrEmpty(hash));
        }

        [Test]
        public void HashPassword_ContainSalt()
        {
            var hash = _passHasher.HashPassword(password);

            var parts = hash.Split(":");
            Assert.True(parts.Length == 2);
        }
        [Test]
        public void HashPassword_SamePassword_HaveDifferentHashes()
        {
            var firstHash = _passHasher.HashPassword(password);
            var secondHash = _passHasher.HashPassword(password);

            Assert.AreNotEqual(firstHash, secondHash);
        }

        [Test]
        public void CheckPassWithHash_IfPasswordAndPasswordHash_ReturnsTrue()
        {
            var hash = _passHasher.HashPassword(password);

            var isMatch = _passHasher.CheckPassWithHash(password, hash);

            Assert.True(isMatch);            
        }

        [Test]
        public void CheckPassWithHash_IfFakePasswordAndPasswordHash_ReturnsFalse()
        {
            var hash = _passHasher.HashPassword(password);

            var isMatch = _passHasher.CheckPassWithHash(fakePassword, hash);
            
            Assert.False(isMatch);
        }
    }
}
