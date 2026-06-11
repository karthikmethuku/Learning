import * as drills from './src/drills.js';
import { runTests } from './spec/tests.js';

// Optional: visible demo of each drill output
console.log("sum(2,3) =>", drills.sum(2, 3));
console.log("greet({name:'Karthik'}) =>", drills.greet({ name: 'Karthik' }));
console.log("tail([1,2,3]) =>", drills.tail([1, 2, 3]));
console.log("merge({a:1},{b:2}) =>", drills.merge({ a: 1 }, { b: 2 }));
console.log("range(3,6) =>", drills.range(3, 6));
console.log("memoized fib(10) =>", drills.memoizedFib(10));

runTests(drills);