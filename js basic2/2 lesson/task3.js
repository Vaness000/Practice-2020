//1
var obj = {};

function A() { return obj; }
function B() { return obj; }

var a = new A;
var b = new B;

alert( a == b );//возможны, возвращают один и тот же объект
//2
function Calculator() {

    this.read = function() {
      this.a = +prompt('a?', 0);
      this.b = +prompt('b?', 0);
    };
  
    this.sum = function() {
      return this.a + this.b;
    };
  
    this.mul = function() {
      return this.a * this.b;
    };
  }
  
  var calculator = new Calculator();
  calculator.read();
  
  alert( "Сумма=" + calculator.sum() );
  alert( "Произведение=" + calculator.mul() );
  //3
  function Accumulator(startingValue) {
    this.value = startingValue;
  
    this.read = function() {
      this.value += +prompt('Сколько добавлять будем?', 0);
    };
  
  }
  
  var accumulator = new Accumulator(1);
  accumulator.read();
  accumulator.read();
  alert( accumulator.value );
  //4
  function Calculator1() {

    var methods = {
      "-": function(a, b) {
        return a - b;
      },
      "+": function(a, b) {
        return a + b;
      }
    };
  
    this.calculate = function(str) {
  
      var split = str.split(' '),
        a = +split[0],
        op = split[1],
        b = +split[2]
  
      if (!methods[op] || isNaN(a) || isNaN(b)) {
        return NaN;
      }
  
      return methods[op](a, b);
    }
  
    this.addMethod = function(name, func) {
      methods[name] = func;
    };
  }
  
  var calc = new Calculator1;
  
  calc.addMethod("*", function(a, b) {
    return a * b;
  });
  calc.addMethod("/", function(a, b) {
    return a / b;
  });
  calc.addMethod("**", function(a, b) {
    return Math.pow(a, b);
  });
  
  var result = calc.calculate("2 ** 3");
  alert( result ); 