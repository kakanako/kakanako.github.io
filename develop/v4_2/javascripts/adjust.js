function addx()
{
    var pivot = document.querySelector('#pivot')
    pivot.object3D.position.x += 0.08
}

// increment y position
function addy()
{
    var pivot = document.querySelector('#pivot')
    pivot.object3D.position.y += 0.08
}

// increment z position
function addz()
{
    var pivot = document.querySelector('#pivot')
    pivot.object3D.position.z += 0.08
}

// decrement x position
function minusx()
{
    var pivot = document.querySelector('#pivot')
    pivot.object3D.position.x -= 0.08
}

// decrement y position
function minusy()
{
    var pivot = document.querySelector('#pivot')
    pivot.object3D.position.y -= 0.08
}

// decrement z position
function minusz()
{
    var pivot = document.querySelector('#pivot')
    pivot.object3D.position.z -= 0.08
}

// rotate x position
function rotatex()
{
    var pivot = document.querySelector('#pivot')
    pivot.object3D.rotation.x += 0.05
}

// rotate y position
function rotatey()
{
    var pivot = document.querySelector('#pivot')
    pivot.object3D.rotation.y += 0.05
}

// rotate z position
function rotatez()
{
    var pivot = document.querySelector('#pivot')
    pivot.object3D.rotation.z += 0.05
}

// add width
function addwidth()
{
    var plane = document.querySelector('#plane')
    var width = plane.getAttribute('width')
    plane.setAttribute('width', width-(0.05*-1))
}

// minus width
function minuswidth()
{
    var plane = document.querySelector('#plane')
    var width = plane.getAttribute('width')
    plane.setAttribute('width', width-0.05)
}

// add height
function addheight()
{
    var plane = document.querySelector('#plane')
    var height = plane.getAttribute('height')
    plane.setAttribute('height', height-(0.05*-1))
}

// minus height
function minusheight()
{
    var plane = document.querySelector('#plane')
    var height = plane.getAttribute('height')
    plane.setAttribute('height', height-0.05)
}

function reset()
{
    var pivot = document.querySelector('#pivot')

    pivot.object3D.rotation.x = 0
    pivot.object3D.rotation.y = 0
    pivot.object3D.rotation.z = 0

    pivot.object3D.position.x = 0
    pivot.object3D.position.y = 0
    pivot.object3D.position.z = -20

    plane.setAttribute('width', 8)
    plane.setAttribute('height', 8)
}