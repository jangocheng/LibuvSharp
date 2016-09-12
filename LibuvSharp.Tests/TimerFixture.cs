using System;
using Xunit;
using Xunit.Extensions;

namespace LibuvSharp.Tests
{
	public class TimerFixture : Fixture
	{
		[Theory]
		[InlineData(10, 10)]
		[InlineData(2,  50)]
		[InlineData(50,  1)]
		public void Simple(int times, int spawn)
		{
			var t = new UVTimer();
			int i = 0;
			t.Tick +=  () => {
				i++;
				if (i > times) {
					t.Close();
				}
			};
			t.Start(TimeSpan.FromMilliseconds(spawn));
			var now = Loop.Current.Now;
			Loop.Current.Run();
			Assert.True(Loop.Current.Now - now >= (ulong)(times * spawn));
			Assert.True(t.IsClosed);
		}
	}
}

