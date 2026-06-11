// Day 1 Drills: arrow functions, destructuring, spread/rest, modules, template literals

// 1) Arrow functions + defaults
export const sum = (a = 0, b = 0) => a + b;

// 2) Object destructuring + template literals
// Input: { name: "Karthik", age?: number }
// Output: "Hello, Karthik!"
export const greet = ({ name }) => `Hello, ${name}!`;

// 3) Array destructuring + rest
// tail([1,2,3]) -> [2,3]; tail([5]) -> []
export const tail = (arr = []) => {
    const [, ...rest] = arr;
    return rest;
};

// 4) Object spread: non-mutating merge (rightmost wins)
export const merge = (a = {}, b = {}) => ({ ...a, ...b });

// 5) Pure function using array methods
// range(3,6) -> [3,4,5,6]
export const range = (start, end) =>
    Array.from({ length: end - start + 1 }, (_, i) => start + i);

// 6) Closures: memoize a pure function (here: fibonacci)
// - Shows why closures matter for React hooks mental model
export const memoizedFib = (() => {
    const cache = new Map([[0, 0], [1, 1]]);
    const fib = (n) => {
        if (cache.has(n)) return cache.get(n);
        const val = fib(n - 1) + fib(n - 2);
        cache.set(n, val);
        return val;
    };
    return fib;
})();

// stretch: implement a simple curry utility (optional)
// curry((a,b,c)=>a+b+c)(1)(2)(3) === 6
export const curry = (fn) => (...args) =>
    args.length >= fn.length ? fn(...args) : (...more) => curry(fn)(...args, ...more);
