using System;
using Xunit;
using Shouldly;

namespace Yobao.Testing
{
    public class SmokeTest
    {
        [Fact]
        public void WillPass()
        {
            1.ShouldBe(1);
        }
    }
}
