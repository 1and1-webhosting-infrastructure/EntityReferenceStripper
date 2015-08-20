﻿using NUnit.Framework;

namespace EntityReferenceStripper
{
    [TestFixture]
    public class SingleSelfRefTest : EntityReferenceTests
    {
        [SetUp]
        public void SetUp()
        {
            var strippedRef = new MockEntity1 {Id = 2};
            EntityWithStrippedRefs = new MockEntity1
            {
                Id = 1,
                FriendlyName = "Foo",
                SingleSelfRef = strippedRef
            };

            var resolvedRef = new MockEntity1 {Id = 2, FriendlyName = "Bar"};
            EntityWithResolvedRefs = new MockEntity1
            {
                Id = 1,
                FriendlyName = "Foo",
                SingleSelfRef = resolvedRef
            };

            EntityResolverMock.Setup(x => x.Resolve(strippedRef, typeof (MockEntity1))).Returns(resolvedRef);
        }
    }
}