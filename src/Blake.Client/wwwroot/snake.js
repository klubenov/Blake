function DisableButtonsAutoFocusOnClick() {
    let buttons = Array.from(document.getElementsByTagName('button'));

    buttons.forEach(button => {
        button.addEventListener('click', function (e) {
            e.target.blur();
        })
    });
}

function UpdateSnake(updateModel) {
    let foodPositionId = `${updateModel.foodPosition[1]}|${updateModel.foodPosition[0]}`;

    let cellsToDeleteIds = [];

    updateModel.cellsToDelete.forEach(cell => {
        cellsToDeleteIds.push(`${cell[1]}|${cell[0]}`);
    });

    let snakeCellIds = [];

    updateModel.newSnakePosition.forEach(cell => {
        snakeCellIds.push(`${cell[1]}|${cell[0]}`);
    });

    cellsToDeleteIds.forEach(id => {
        let cellToDeleteElement = document.getElementById(id);
        cellToDeleteElement.classList.remove('food-segment');
        cellToDeleteElement.classList.remove('snake-segment');
    })

    let foodCellElement = document.getElementById(foodPositionId);
    if (!foodCellElement.classList.contains('food-segment')) {
        foodCellElement.classList.add('food-segment');
    }

    snakeCellIds.forEach(id => {
        let snakeCell = document.getElementById(id);
        snakeCell.classList.remove('food-segment');
        if (!snakeCell.classList.contains('snake-segment')) {
            snakeCell.classList.add('snake-segment');
        }

    });
}

function SetDifficulty(difficulty) {
    let snakeBoard = document.getElementsByClassName('snake-board')[0];
    snakeBoard.classList.remove('difficulty-easy');
    snakeBoard.classList.remove('difficulty-medium');
    snakeBoard.classList.remove('difficulty-hard');
    snakeBoard.classList.add(difficulty);

    let button = document.getElementById(difficulty);
    button.classList.add('selected');
}

function ResetButtons(className) {
    let buttons = Array.from(document.getElementsByClassName(className));

    buttons.forEach(button => {
        button.classList.remove('selected');
        if (button.getAttribute('disabled') != null) {
            button.removeAttribute('disabled');
        }
    });
}

function ToggleButtons(className) {
    let buttons = Array.from(document.getElementsByClassName(className));

    buttons.forEach(button => {
        if (button.classList.contains('selected')) {
            button.classList.remove('selected');
        } else {
            button.classList.add('selected');
        }
    })
}

function DisableButtons(className) {
    let buttons = Array.from(document.getElementsByClassName(className));

    buttons.forEach(button => {
        button.setAttribute('disabled', true);
    });
}

function SetTheme(theme) {
    let snakeContainer = document.getElementsByClassName('snake-container')[0];

    let segments = Array.from(document.getElementsByClassName('segment'));

    ReplaceTheme(snakeContainer, theme);

    if (segments.length > 0) {
        segments.forEach(segment => {
            ReplaceTheme(segment, theme);
        });
    }

    let button = document.getElementById(theme);
    button.classList.add('selected');
}

function ReplaceTheme(obj, theme) {
    obj.classList.forEach(className => {
        if (className.includes('theme')) {
            obj.classList.replace(className, theme);
        }
    });
}

function PlaySound(soundId) {
    let soundElement = document.getElementById(soundId);
    soundElement.play();
}

function GameOver(message) {
    let player = prompt(message);
    if (player == null) {
        alert('No name, no gain!');
    }
    return window.Snake.invokeMethodAsync('SaveScore', player);
}

function SetJsReference(objectReference) {
    window.Snake = objectReference;
}

window.addEventListener('keydown', function (e) {
    return window.Snake.invokeMethodAsync('HandleControl', e.code);
})