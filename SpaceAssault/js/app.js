// A cross-browser requestAnimationFrame
// See https://hacks.mozilla.org/2011/08/animating-with-javascript-from-setinterval-to-requestanimationframe/
var requestAnimFrame = (function(){
    return window.requestAnimationFrame       ||
        window.webkitRequestAnimationFrame ||
        window.mozRequestAnimationFrame    ||
        window.oRequestAnimationFrame      ||
        window.msRequestAnimationFrame     ||
        function(callback){
            window.setTimeout(callback, 1000 / 60);
        };
})();

// Create the canvas
var canvas = document.createElement("canvas");
var ctx = canvas.getContext("2d");
canvas.width = 512;
canvas.height = 480;
document.body.appendChild(canvas);

// The main game loop
var lastTime;
function main() {
    var now = Date.now();
    var dt = (now - lastTime) / 1000.0;

    update(dt);
    render();

    lastTime = now;
    requestAnimFrame(main);
};

resources.load([
    'img/sprites.png',
    'img/terrain.png'
]);
resources.onReady(init);

function init() {
    terrainPattern = ctx.createPattern(resources.get('img/terrain.png'), 'repeat');

    document.getElementById('play-again').addEventListener('click', function() {
        reset();
    });
    reset();
    lastTime = Date.now();
    main();
}

// Game state
var player = {
    pos: [0, 0],
    sprite: new Sprite('img/sprites.png', [0, 0], [39, 39], 16, [0, 1]),
    
};

var bullets = [];
var enemies = [];
var explosions = [];
var mannaExplosions = [];
var megaliths = [];
var manna = [];
var megalithsCount = RandomFromInterval(3,5);
var mannaCount = RandomFromInterval(3,8);

function RandomFromInterval(min,max){
    min = Math.ceil(min);
    max = Math.floor(max);
    return Math.floor(Math.random() * (max - min + 1)) + min;
}
//создание мегалитов и манны
function CreateMegalith(){
    do{
        AddMegalith();
    }while(megaliths.length<megalithsCount)
    
    
    
    /*for(var i = 0;i<megalithsCount;i++){
        //do{
            positionX = RandomFromInterval(70,470);
            positionY = RandomFromInterval(70,410);//boxCollides(player.pos,player.sprite.size,[positionX,positionY],spriteSize) &&
        //}while( CollidesMegaliths(i,positionX,positionY) )
        megaliths.push({
            pos:[positionX,positionY],
            sprite: new Sprite('img/sprites.png',positionOnSprite,spriteSize,1,[0,1],'vertical',false) 
        });
    }
    /*for(var i = 0;i<mannaCount;i++){
            positionX = RandomFromInterval(70,470);
            positionY = RandomFromInterval(70,410);//boxCollides(player.pos,player.sprite.size,[positionX,positionY],spriteSize) &&
        //}while( CollidesMegaliths(i,positionX,positionY) )
        manna.push({
            pos:[positionX,positionY],
            sprite: new Sprite('img/sprites.png',[0,165],[55,45],2, [0, 1]) 
        });
        
    }*/
}
function AddMegalith(){
    var positionX = RandomFromInterval(70,470);
    var positionY = RandomFromInterval(70,410);
    for (var i = 0;i<megaliths.length;i++){
        if(boxCollides([positionX,positionY],[55,50],megaliths[i].pos,megaliths[i].sprite.size)){
            return;
        }
        
    }

    return megaliths.push({
        pos:[positionX,positionY],
        sprite: new Sprite('img/sprites.png',[0,217],[55,50],1,[0,1],'vertical',false)
    });
}
function CreateManna(){
   
    do{
        AddMana();
    }while(manna.length<mannaCount)
}
function AddMana(){
    var positionX = RandomFromInterval(70,470);
    var positionY = RandomFromInterval(70,410);


    for (var i = 0;i<megaliths.length;i++){
        if(boxCollides([positionX,positionY],[55,50],megaliths[i].pos,megaliths[i].sprite.size)){
            return;
        }
        
    }
    for (var i = 0;i<manna.length;i++){
        if(boxCollides([positionX,positionY],[55,45],manna[i].pos,manna[i].sprite.size)){
            return;
        }
        
    }
    return manna.push({
        pos:[positionX,positionY],
        sprite: new Sprite('img/sprites.png',[0,165],[55,45],2, [0, 1]) 
});
}



//мегалиты друг на друге?
function CollidesMegaliths(iteration,currentX,currentY){
    
    if(iteration == 0){
        return false;
    }else{
        var pos = megaliths[iteration-1].pos;
        return (Math.abs(currentX-pos[0])>200 && Math.abs(currentY-pos[1])>200);
    }
}


var lastFire = Date.now();
var gameTime = 0;
var isGameOver;
var terrainPattern;

// The score
var score = 0;
var scoreMana = 0;
var scoreM = document.getElementById('mana');
var scoreEl = document.getElementById('score');

// Speed in pixels per second
var playerSpeed = 200;
var bulletSpeed = 500;
var enemySpeed = 100;

// Update game objects
function update(dt) {
    gameTime += dt;

    handleInput(dt);
    updateEntities(dt);

    // It gets harder over time by adding enemies using this
    // equation: 1-.993^gameTime
    if(Math.random() < 1 - Math.pow(.993, gameTime)) {
        enemies.push({
            pos: [canvas.width,
                  Math.random() * (canvas.height - 39)],
            sprite: new Sprite('img/sprites.png', [0, 78], [80, 39],
                               6, [0, 1, 2, 3, 2, 1])
        });
    }
    if ((manna.length < 8) && (Math.random() < 1 - Math.pow(.993, gameTime)))
    {
        AddMana();
    }

    checkCollisions(dt);

    scoreEl.innerHTML = score;
    scoreM.innerHTML = scoreMana;
};


//проверка на столкновение игрока и мегалита
function PlayerNearMegalith()
{
    for (var i = 0; i < megaliths.length; i++)
    {

        if (boxCollides(player.pos, player.sprite.size
            , megaliths[i].pos, megaliths[i].sprite.size))
        {
            return megaliths[i];
        }
    }

    return false;
}
//если столкновение произошло и дальнейшее движение невозможно, то выход из функции
function handleInput(dt) {
    if(input.isDown('DOWN') || input.isDown('s')) {
        if(PlayerNearMegalith()!= false){
        if ((PlayerNearMegalith().pos[1] > player.pos[1]))
            return;
        }
        player.pos[1] += playerSpeed * dt;
    }

    if(input.isDown('UP') || input.isDown('w')) {
        if(PlayerNearMegalith()!= false){
        if ((PlayerNearMegalith().pos[1] < player.pos[1]))
            return;
        }
        player.pos[1] -= playerSpeed * dt;
    }

    if(input.isDown('LEFT') || input.isDown('a')) {

        if(PlayerNearMegalith()!= false){
        if ((PlayerNearMegalith().pos[0] < player.pos[0]))
            return;
        }
        player.pos[0] -= playerSpeed * dt;
    }

    if(input.isDown('RIGHT') || input.isDown('d')) {
        if(PlayerNearMegalith()!= false){
        if ((PlayerNearMegalith().pos[0] > player.pos[0]))
            return;
        }
        player.pos[0] += playerSpeed * dt;
    }

    if(input.isDown('SPACE') &&
       !isGameOver &&
       Date.now() - lastFire > 100) {
        var x = player.pos[0] + player.sprite.size[0] / 2;
        var y = player.pos[1] + player.sprite.size[1] / 2;

        bullets.push({ pos: [x, y],
                       dir: 'forward',
                       sprite: new Sprite('img/sprites.png', [0, 39], [18, 8]) });
        bullets.push({ pos: [x, y],
                       dir: 'up',
                       sprite: new Sprite('img/sprites.png', [0, 50], [9, 5]) });
        bullets.push({ pos: [x, y],
                       dir: 'down',
                       sprite: new Sprite('img/sprites.png', [0, 60], [9, 5]) });

        lastFire = Date.now();
    }
}

function updateEntities(dt) {
    // Update the player sprite animation
    player.sprite.update(dt);

    // Update all the bullets
    for(var i=0; i<bullets.length; i++) {
        var bullet = bullets[i];

        switch(bullet.dir) {
        case 'up': bullet.pos[1] -= bulletSpeed * dt; break;
        case 'down': bullet.pos[1] += bulletSpeed * dt; break;
        default:
            bullet.pos[0] += bulletSpeed * dt;
        }

        // Remove the bullet if it goes offscreen
        if(bullet.pos[1] < 0 || bullet.pos[1] > canvas.height ||
           bullet.pos[0] > canvas.width) {
            bullets.splice(i, 1);
            i--;
        }
    }
    for(var i = 0;i<megaliths.length;i++){
        megaliths[i].sprite.update(dt);
    }
    for(var i = 0;i<manna.length;i++){
        manna[i].sprite.update(dt);
    }

    // Update all the enemies
    for(var i=0; i<enemies.length; i++) {
        enemies[i].pos[0] -= enemySpeed * dt;
        enemies[i].sprite.update(dt);

        // Remove if offscreen
        if(enemies[i].pos[0] + enemies[i].sprite.size[0] < 0) {
            enemies.splice(i, 1);
            i--;
        }
    }

    // Update all the explosions
    for(var i=0; i<explosions.length; i++) {
        explosions[i].sprite.update(dt);

        // Remove if animation is done
        if(explosions[i].sprite.done) {
            explosions.splice(i, 1);
            i--;
        }
    }
    for (var i = 0;i<mannaExplosions.length;i++){
        mannaExplosions[i].sprite.update(dt);
        if(mannaExplosions[i].sprite.done){
            mannaExplosions.splice(i,1);
            i--;
        }
    }
    for (var i = 0;i<manna.length;i++){
        var mannaPos = manna[i].pos;
        var mannaSize = manna[i].sprite.size;

        if(boxCollides(player.pos,player.sprite.size,mannaPos,mannaSize)){
            manna.splice(i,1);
            scoreMana++;
            {
                    mannaExplosions.push({
                        pos: mannaPos,
                        sprite: new Sprite('img/sprites.png',[0,165],[55,45],10, [0, 1,2,3],null,
                        true)
                    });
                
            }
        }
    }
    
}

// Collisions

function collides(x, y, r, b, x2, y2, r2, b2) {
    return !(r <= x2 || x > r2 ||
             b <= y2 || y > b2);
}

function boxCollides(pos, size, pos2, size2) {
    return collides(pos[0], pos[1],
                    pos[0] + size[0], pos[1] + size[1],
                    pos2[0], pos2[1],
                    pos2[0] + size2[0], pos2[1] + size2[1]);
}

function checkCollisions(dt) {
    checkPlayerBounds();
    CheckEnemyBounds(dt);
    
    // Run collision detection for all enemies and bullets
    for(var i=0; i<enemies.length; i++) {
        var pos = enemies[i].pos;
        var size = enemies[i].sprite.size;
        // проверка на столкновене мегалита и пули
        isHit:for(var j=0; j<bullets.length; j++) {
            var pos2 = bullets[j].pos;
            var size2 = bullets[j].sprite.size;

            for (var k = 0;k<megaliths.length;k++){
                var posMeg = megaliths[k].pos;
                var sizeMeg = megaliths[k].sprite.size;
                //если пуля и мегалит столкнулись то пуля удаляется 
                //из массива и переход к проверке следующей пули
                if(boxCollides(pos2,size2,posMeg,sizeMeg)){
                    bullets.splice(j,1);
                    break isHit;
                }
            }

            if(boxCollides(pos, size, pos2, size2)) {
                // Remove the enemy
                enemies.splice(i, 1);
                i--;

                // Add score
                score += 100;

                // Add an explosion
                explosions.push({
                    pos: pos,
                    sprite: new Sprite('img/sprites.png',
                                       [0, 117],
                                       [39, 39],
                                       16,
                                       [0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12],
                                       null,
                                       true)
                });

                // Remove the bullet and stop this iteration
                bullets.splice(j, 1);
                break;
            }
        }

        if(boxCollides(pos, size, player.pos, player.sprite.size)) {
            gameOver();
        }
    }
    
}

function checkPlayerBounds() {
    // Check bounds
    if(player.pos[0] < 0) {
        player.pos[0] = 0;
    }
    else if(player.pos[0] > canvas.width - player.sprite.size[0]) {
        player.pos[0] = canvas.width - player.sprite.size[0];
    }

    if(player.pos[1] < 0) {
        player.pos[1] = 0;
    }
    else if(player.pos[1] > canvas.height - player.sprite.size[1]) {
        player.pos[1] = canvas.height - player.sprite.size[1];
    }
    var currentPosY= player.pos[1];

    for(var i = 0;i<megaliths.length;i++){
        var megPos = megaliths[i].pos;
    }

}
//обход мегалитов кораблями
function CheckEnemyBounds(dt){
    for(var i = 0;i<enemies.length;i++){
        var enemyPos = enemies[i].pos;
        var enemySize = enemies[i].sprite.size;
        for(var j = 0;j<megaliths.length;j++){
            var megalithPos = megaliths[j].pos;
            var megalithSize = megaliths[j].sprite.size;
            if(boxCollides([enemyPos[0]-55,enemyPos[1]],enemySize,megalithPos,megalithSize)){
                enemies[i].pos[1]-=enemySpeed * dt;
            } 
        }
    }
}

// Draw everything
function render() {
    ctx.fillStyle = terrainPattern;
    ctx.fillRect(0, 0, canvas.width, canvas.height);

    // Render the player if the game isn't over
    if(!isGameOver) {
        renderEntity(player);
    }
    renderEntities(megaliths);
    renderEntities(manna);
    renderEntities(bullets);
    renderEntities(enemies);
    renderEntities(explosions);
    renderEntities(mannaExplosions);
    
};

function renderEntities(list) {
    for(var i=0; i<list.length; i++) {
        renderEntity(list[i]);
    }    
}

function renderEntity(entity) {
    ctx.save();
    ctx.translate(entity.pos[0], entity.pos[1]);
    entity.sprite.render(ctx);
    ctx.restore();
}

// Game over
function gameOver() {
    document.getElementById('game-over').style.display = 'block';
    document.getElementById('game-over-overlay').style.display = 'block';
    isGameOver = true;
}

// Reset game to original state
function reset() {
    document.getElementById('game-over').style.display = 'none';
    document.getElementById('game-over-overlay').style.display = 'none';
    isGameOver = false;
    gameTime = 0;
    score = 0;
    scoreMana = 0;
    megaliths=[];
    manna = [];
    mannaExplosions = [];
    CreateManna();
    CreateMegalith();
    enemies = [];
    bullets = [];
    

    player.pos = [50, canvas.height / 2];
};
