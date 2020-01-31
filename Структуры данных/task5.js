//1
var a = {}
alert(isEmpty(a));
a.b="2";
alert(isEmpty(a));
//2
var salaries = {
  "Вася": 100,
    "Петя": 300,
    "Даша": 250
}
var sum = 0;
for (var name in salaries) {
  sum += salaries[name];
}
alert( sum );
//3
var max = 0;
var maxName = "";
for (var name in salaries) {
  if (max < salaries[name]) {
    max = salaries[name];
    maxName = name;
}
}
alert( maxName || "нет сотрудников" );
//4
var menu = {
  width: 200,
  height: 300,
  title: "My menu"
};

function isNumeric(n) {
  return !isNaN(parseFloat(n)) && isFinite(n);
}

function multiplyNumeric(obj) {
  for (var key in obj) {
    if (isNumeric(obj[key])) {
      obj[key] *= 2;
    }
  }
}

multiplyNumeric(menu);

alert( "menu width=" + menu.width + " height=" + menu.height + " title=" + menu.title );
function isEmpty(obj) {
  for (var key in obj) {
    return false;
  }
  return true;
}
