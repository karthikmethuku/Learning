// Tiny test harness – no deps
const el = (id) => document.getElementById(id);

function assert(cond, msg) {
    const row = document.createElement('div');
    row.textContent = (cond ? "✓ " : "✗ ") + msg;
    row.className = cond ? "pass" : "fail";
    el('results').appendChild(row);
    if (!cond) console.error("FAIL:", msg);
}

export function runTests(d) {
    assert(d.sum(2, 3) === 5, "sum adds numbers");
    assert(d.greet({ name: "Karthik" }).includes("Karthik"), "greet uses name via destructuring");
    assert(JSON.stringify(d.tail([1, 2, 3])) === JSON.stringify([2, 3]), "tail returns array without first element");
    assert(JSON.stringify(d.merge({ a: 1 }, { a: 2, b: 3 })) === JSON.stringify({ a: 2, b: 3 }), "merge spreads with rightmost override");
    assert(JSON.stringify(d.range(3, 6)) === JSON.stringify([3, 4, 5, 6]), "range creates inclusive list");
    assert(d.memoizedFib(10) === 55, "memoized fib(10) is 55");

    // stretch tests
    const curried = d.curry((a, b, c) => a + b + c);
    assert(curried(1)(2)(3) === 6, "curry works for 1-arg steps");
    assert(curried(1, 2)(3) === 6, "curry works for 2-arg + 1-arg");
}
