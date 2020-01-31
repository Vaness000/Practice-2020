//1
var i = 3;

while (i) {
  alert( i-- );
}//1

//2
var i = 0;
while (++i < 5) alert( i );// 1 2 3 4
var i = 0;
while (i++ < 5) alert( i );//1 2 3 4 5

//3
for (var i = 0; i < 5; i++) alert( i );//0 1 2 3 4
for (var i = 0; i < 5; ++i) alert( i );//0 1 2 3 4

//4
for(var i = 2; i < 11; i+=2)alert( i );

//5
var i = 0;
while (i < 3) {
  alert( "номер " + i + "!" );
  i++;
}

//6
var num;

do {
  num = prompt("Введите число больше 100?", 0);
} while (num <= 100 && num != null);

//7
nextPrime:
for (var i = 2; i <= 10; i++) {

    for (var j = 2; j < i; j++) {
      if (i % j == 0) continue nextPrime;
    }

    alert( i ); // простое
  }
