﻿using NUnit.Framework;

namespace Dehydrator
{
    [TestFixture]
    public class MultiRefTest : EntityReferenceTests<MockEntity2>
    {
        [SetUp]
        public void SetUp()
        {
            DehydratedRef = new MockEntity2 {Id = 2};
            EntityWithDehydratedRefs = new MockEntity1
            {
                Id = 1,
                FriendlyName = "Foo",
                MultiRef = {DehydratedRef}
            };

            ResolvedRef = new MockEntity2 {Id = 2, FriendlyName = "Bar"};
            EntityWithResolvedRefs = new MockEntity1
            {
                Id = 1,
                FriendlyName = "Foo",
                MultiRef = {ResolvedRef}
            };
        }
    }
}
