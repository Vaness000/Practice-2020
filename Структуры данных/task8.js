//1
function addClass(obj, cls) {
  var classes = obj.className ? obj.className.split(' ') : [];

  for (var i = 0; i < classes.length; i++) {
    if (classes[i] == cls) return;
  }

  classes.push(cls);

  obj.className = classes.join(' ');
}

var obj = {
  className: 'open menu'
};

addClass(obj, 'new');
addClass(obj, 'open');
addClass(obj, 'me');
alert(obj.className)
//2
function camelize(str) {
  var arr = str.split('-');

  for (var i = 1; i < arr.length; i++) {
    arr[i] = arr[i].charAt(0).toUpperCase() + arr[i].slice(1);
  }

  return arr.join('');
}

alert( camelize("background-color") );
alert( camelize("list-style-image") );
alert( camelize("-webkit-transition") );
//3
function removeClass(obj, cls) {
  var classes = obj.className.split(' ');

  for (var i = 0; i < classes.length; i++) {
    if (classes[i] == cls) {
      classes.splice(i, 1); // удалить класс
      i--; // (*)
    }
  }
  obj.className = classes.join(' ');

}
removeClass(obj,'me');
alert(obj.className);
//4
function filterRangeInPlace(arr, a, b) {

  for (var i = 0; i < arr.length; i++) {
    var val = arr[i];
    if (val < a || val > b) {
      arr.splice(i--, 1);
    }
  }

}
var myArr = [5, 3, 8, 1];
filterRangeInPlace(myArr,1,4);
alert(myArr);
//5
var myArr1 = [5, 2, 1, -10, 8];
function compareReversed(a, b) {
  return b - a;
}

myArr1.sort(compareReversed);

alert( myArr1 );
//6
var arr = ["HTML", "JavaScript", "CSS"];
var arrSorted = arr.slice().sort();
alert(arr);
alert(arrSorted);
//7
var arr1 = [1, 2, 3, 4, 5];
function compareRandom(a, b) {
  return Math.random() - 0.5;
}

arr1.sort(compareRandom);

alert( arr1 );
//8
var vasya = { name: "Вася", age: 23 };
var masha = { name: "Маша", age: 18 };
var vovochka = { name: "Вовочка", age: 6 };

var people = [ vasya , masha , vovochka ];

function compareAge(personA, personB) {
  return personA.age - personB.age;
}
people.sort(compareAge);
for(var i = 0; i < people.length; i++) {
  alert(people[i].name);
}
//9
var list = {
  value: 1,
  next: {
    value: 2,
    next: {
      value: 3,
      next: {
        value: 4,
        next: null
      }
    }
  }
};

function printList(list) {
  var tmp = list;
  while (tmp) {
    alert( tmp.value );
    tmp = tmp.next;
  }
}

printList(list);
//10
function aclean(arr) {
  var obj = {};

  for (var i = 0; i < arr.length; i++) {
    var sorted = arr[i].toLowerCase().split('').sort().join('');

    obj[sorted] = arr[i];
  }

  var result = [];

  for (var key in obj) result.push(obj[key]);

  return result;
}

var arr2 = ["воз", "киборг", "корсет", "ЗОВ", "гробик", "костер", "сектор"];

alert( aclean(arr2) );

//11
function unique(arr) {
  var obj = {};

  for (var i = 0; i < arr.length; i++) {
    var str = arr[i];
    obj[str] = true;
  }

  return Object.keys(obj); 
}

var strings = ["кришна", "кришна", "харе", "харе",
  "харе", "харе", "кришна", "кришна", "8-()"
];

alert( unique(strings) );
