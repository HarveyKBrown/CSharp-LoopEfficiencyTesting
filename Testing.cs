using System;
using System.Diagnostics;
using System.Linq;
using System.Collections.Generic;

class Testing {

	class TestCalculator {
		public void DoWork() {
			float a = (float)Math.Sqrt(Math.PI);
		}
	}

	Stopwatch watch = new System.Diagnostics.Stopwatch();
	const int ITERATIONS = 100000000;

	TestCalculator[] testData = new TestCalculator[ITERATIONS];
	List<TestCalculator> testDataList = new List<TestCalculator>();

	public Testing() {
		for (int i = 0; i < ITERATIONS; i++) {
			testData[i] = new TestCalculator();
			testDataList.Add(testData[i]);
		}
	}

	static void Main(string[] args) {

		Testing t = new Testing();
		t.Test("basic for loop", () => {
			for (int i = 0; i < t.testData.Length; i++) {
				t.testData[i].DoWork();
			}
		});
		t.Test("for loop with length cacheing", () => {
			for (int i = 0, counti = t.testData.Length; i < counti; i++) {
				t.testData[i].DoWork();
			}
		});
		t.Test("ForEach loop", () => {
			foreach (TestCalculator c in t.testData) {
				c.DoWork();
			}
		});
		t.Test("ToList ForEach", () => {
			t.testData.ToList().ForEach(c => c.DoWork());
		});
		t.Test("basic for loop (list)", () => {
			for (int i = 0; i < t.testDataList.Count; i++) {
				t.testDataList[i].DoWork();
			}
		});
		t.Test("for loop with length cacheing (list)", () => {
			for (int i = 0, counti = t.testDataList.Count; i < counti; i++) {
				t.testDataList[i].DoWork();
			}
		});
		t.Test("ForEach (native, list)", () => {
			t.testDataList.ForEach(c => c.DoWork());
		});
	}

	public void PrintTime(string ident) {
		Console.WriteLine("{0}: {1} ms", ident, watch.ElapsedMilliseconds);
	}

	public void Test(string ident, Action f) {
		watch.Reset();
		watch.Start();
		f();
		watch.Stop();
		PrintTime(ident);
	}
}