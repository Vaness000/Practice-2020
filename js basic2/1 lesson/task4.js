//1
function sum(a) {

    return function(b) {
      return a + b;
    };
  
  }
  
  alert( sum(1)(2) );//3
  alert( sum(5)(-1) );//4
//2
function makeBuffer() {
  var text = '';

  function buffer(piece) {
    if (arguments.length == 0) { 
      return text;
    }
    text += piece;
  };

  buffer.clear = function() { //для третьего задания
    text = "";
  }

  return buffer;
};


var buffer = makeBuffer();

buffer('Замыкания');
buffer(' Использовать');
buffer(' Нужно!');
alert( buffer() ); // 'Замыкания Использовать Нужно!'

var buffer2 = makeBuffer();
buffer2(0);
buffer2(1);
buffer2(0);

alert( buffer2() ); // '010'
//3
buffer.clear();

alert( buffer() );
//4
var users = [{
  name: "Вася",
  surname: 'Иванов',
  age: 20
}, {
  name: "Петя",
  surname: 'Чапаев',
  age: 25
}, {
  name: "Маша",
  surname: 'Медведева',
  age: 18
}];

function byField(field) {
    return function(a, b) {
      return a[field] > b[field] ? 1 : -1;
    }
  }

users.sort(byField('name'));
users.forEach(function(user) {
  alert( user.name );
});

users.sort(byField('age'));
users.forEach(function(user) {
  alert( user.name );
});
//5
function filter(arr, func) {
  var result = [];

  for (var i = 0; i < arr.length; i++) {
    var val = arr[i];
    if (func(val)) {
      result.push(val);
    }
  }

  return result;
}
function inBetween(a, b) {
  return function(x) {
    return x >= a && x <= b;
  };
}
function inArray(arr) {
  return function(x) {
    return arr.indexOf(x) != -1;
  };
}

var arr = [1, 2, 3, 4, 5, 6, 7];

alert(filter(arr, function(a) {
  return a % 2 == 0;
}));
alert( filter(arr, inBetween(3, 6)) );
alert( filter(arr, inArray([1, 2, 10])) );
//6
function makeArmy() {

  var shooters = [];

  for (var i = 0; i < 10; i++) {

    var shooter = function me() {
      alert( me.i );
    };
    shooter.i = i;

    shooters.push(shooter);
  }

  return shooters;
}

var army = makeArmy();

army[0](); // 0
army[5](); // 5