//1
var arr = ["Есть", "жизнь", "на", "Марсе"];
var wordsLength = arr.map(function(item) {
  return item.length;
});

alert(wordsLength);
//2
function getSums(arr) {
  var result = [];
  if (!arr.length) return result;

  var totalSum = arr.reduce(function(sum, item) {
    result.push(sum);
    return sum + item;
  });
  result.push(totalSum);

  return result;
}

alert(getSums([1,2,3,4,5]));

alert(getSums([2,3,6,8]));
