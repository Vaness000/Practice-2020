//1
alert( 6.35.toFixed(1) );//хранение с потерей точности как 6.34999999... округление вниз
//2
alert(( 0.1 + 0.2).toFixed(2) + '$' );
//3
/*var i = 0;
while (i != 10) {
  i += 0.2;
}*///i никогда не будет 10, изза потери точности
//4
alert(fibBinet(77));//результаты разные так как тут присутствуют ошибки в округлении
alert(fib(77));
//5
var max = 10;

alert( Math.random() * max );
//6
var min = 5,
  max = 10;

alert( min + Math.random() * (max - min) );

//7
alert(randomInteger(5,8));
function randomInteger(min, max) {
    var rand = min - 0.5 + Math.random() * (max - min + 1)
    rand = Math.round(rand);
    return rand;
  }

function fibBinet(n) {
  var phi = (1 + Math.sqrt(5)) / 2;
  // используем Math.round для округления до ближайшего целого
  return Math.round(Math.pow(phi, n) / Math.sqrt(5));
}

function fib(n) {
  var a = 1,
    b = 0,
    x;
  for (i = 0; i < n; i++) {
    x = a + b;
    a = b
    b = x;
  }
  return b;
}
function fib(n) {
  var a = 1,
    b = 0,
    x;
  for (i = 0; i < n; i++) {
    x = a + b;
    a = b
    b = x;
  }
  return b;
}
