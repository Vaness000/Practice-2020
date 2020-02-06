//1
function f() {
    alert(1)
  }
  
  var obj = {
    f: function() {
      alert(2)//вызовется эта функция
    }
  };
  
  with(obj) {
    f();
  }
//2
var a = 1;

var obj = {
  b: 2
};

with(obj) {
  var b;
  alert( a + b );
}//3