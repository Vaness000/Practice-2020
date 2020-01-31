//1
alert( sumTo(100) );
alert( sumTo1(100) );
alert( sumTo2(100) );
//2
alert( factorial(5) );
//3
alert( fib(7) );
function sumTo(n) {
  var sum = 0;
  for (var i = 1; i <= n; i++) {
    sum += i;
  }
  return sum;
}
function sumTo1(n) {
  if (n == 1) return 1;
  return n + sumTo(n - 1);
}
function sumTo2(n) {
  return n * (n + 1) / 2;
}
function factorial(n) {
  return (n != 1) ? n * factorial(n - 1) : 1;
}
function fib(n) {
  var a = 1,
    b = 1;
  for (var i = 3; i <= n; i++) {
    var c = a + b;
    a = b;
    b = c;
  }
  return b;
}
