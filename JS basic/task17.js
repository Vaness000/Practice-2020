checkAge(16);
checkAge1(12);
checkAge2(16);
checkAge3(12);
var a = 3;
var b = 1;

alert(min(a,b));
alert(pow(3,3));
//1
function checkAge(age) {
  if (age > 18) {
    return true;
  }
  return confirm('Родители разрешили?');
}
function checkAge1(age) {
  if (age > 18) {
    return true;
  } else {
    return confirm('Родители разрешили?');
  }
}//работают одинаково
//2
function checkAge2(age) {
  return (age > 18) ? true : confirm('Родители разрешили?');
}
function checkAge3(age) {
  return (age > 18) || confirm('Родители разрешили?');
}
//3
function min(a, b) {
  if (a < b) {
    return a;
  } else {
    return b;
  }
}
//4
function pow(x, n) {
  var result = x;
  if (n <= 1) {
    alert('Степень ' + n +
      'не поддерживается, введите целую степень, большую 1'
    );
  }
  else
  {
  for (var i = 1; i < n; i++) {
    result *= x;
  }

  return result;
  }
}
