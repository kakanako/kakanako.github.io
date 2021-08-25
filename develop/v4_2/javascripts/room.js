var isRoomCreated = false
var viewStarted = false
var roomRotationY = 0

window.onload = (event) => {
    var adjustControl = document.getElementById('adjust')
    var createControl = document.getElementById('create')
    adjustControl.style.display = "none"
    createControl.style.display = "none"
    setWallVisibility(false)
    setPlaneVisibility(false)
  };

AFRAME.registerComponent('rotate-test', {
    init: function () {},

    tick: function () {
        var rigid = document.querySelector('#roomrig')
        if(!isRoomCreated)
        {
            rigid.object3D.rotation.x = this.el.object3D.rotation.x
            rigid.object3D.rotation.y = this.el.object3D.rotation.y //+= 0.01
            rigid.object3D.rotation.z = this.el.object3D.rotation.z
        }
        else
        {
            if(viewStarted)
                rigid.object3D.rotation.y += 0.02              
        }
    }
})

// Stream from camera 
navigator.mediaDevices.getUserMedia({audio: false, video:  {
    facingMode: "environment",
}})
.then(stream => {
let $video = document.querySelector('video')
$video.srcObject = stream
$video.onloadedmetadata = () => {
$video.play()
}
})

function centralizedMode(){
    var adjustControl = document.getElementById('adjust')
    var createControl = document.getElementById('create')
    var adjustToggle = document.getElementById('adjustmentToggle')

    if (document.getElementById('centralizedToggle').checked) 
    {
        createControl.style.display = "block"

        if(!isRoomCreated)
        {
            isRoomCreated = true
            create()
        }

        setWallVisibility(true)
        adjustToggle.disabled = true
    } else {
        //isRoomCreated = false
        setWallVisibility(false)
        setPlaneVisibility(false)
        createControl.style.display = "none"
        adjustControl.style.display = "none"
        adjustToggle.disabled = false
    }
}

function adjustmentMode(){
    var adjustControl = document.getElementById('adjust')
    var createControl = document.getElementById('create')
    var createToggle = document.getElementById('centralizedToggle')
    if (document.getElementById('adjustmentToggle').checked) 
    {
        adjustControl.style.display = "block"
        createToggle.disabled = true
        setPlaneVisibility(true)
        isRoomCreated = false
    } else {
        createWalls()
        isRoomCreated = true
        setWallVisibility(false)
        setPlaneVisibility(false)
        createControl.style.display = "none"
        adjustControl.style.display = "none"
        createToggle.disabled = false
    }
}

function view()
{
    if(isRoomCreated)
        viewStarted = true
}

function stopview()
{
    viewStarted = false
    var rigid = document.querySelector('#roomrig')
    rigid.object3D.rotation.y = roomRotationY
}

function setPlaneVisibility(isVisible)
{
    var plane = document.querySelector('#plane')
    var cpivot = document.querySelector('#centerpivot')

    cpivot.setAttribute('visible', isVisible)
    plane.setAttribute('visible', isVisible)

    if(isVisible)
    {
        isRoomCreated = false
    }
}

function setWallVisibility(isVisible)
{
    var frontleft = document.querySelector('#frontleft')
    var frontright = document.querySelector('#frontright')
    var frontup = document.querySelector('#frontup')
    var frontdown = document.querySelector('#frontdown')
    var ceiling = document.querySelector('#ceiling')
    var floor = document.querySelector('#floor')
    var leftwall = document.querySelector('#leftwall')
    var rightwall = document.querySelector('#rightwall')
    var backwall = document.querySelector('#backwall')

    frontright.setAttribute('visible', isVisible)
    frontleft.setAttribute('visible', isVisible)
    frontup.setAttribute('visible', isVisible)
    frontdown.setAttribute('visible', isVisible)
    floor.setAttribute('visible', isVisible)
    ceiling.setAttribute('visible', isVisible)
    rightwall.setAttribute('visible', isVisible)
    leftwall.setAttribute('visible', isVisible)
    backwall.setAttribute('visible', isVisible)

    if(isVisible)
    {
        isRoomCreated = true
    }
}

function black()
{
    var frontleft = document.querySelector('#frontleft')
    var frontright = document.querySelector('#frontright')
    var frontup = document.querySelector('#frontup')
    var frontdown = document.querySelector('#frontdown')
    var ceiling = document.querySelector('#ceiling')
    var floor = document.querySelector('#floor')
    var leftwall = document.querySelector('#leftwall')
    var rightwall = document.querySelector('#rightwall')
    var backwall = document.querySelector('#backwall')

    frontright.setAttribute('material', 'color', 'black')
    frontleft.setAttribute('material', 'color', 'black')
    frontup.setAttribute('material', 'color', 'black')
    frontdown.setAttribute('material', 'color', 'black')
    floor.setAttribute('material', 'color', 'black')
    ceiling.setAttribute('material', 'color', 'black')
    rightwall.setAttribute('material', 'color', 'black')
    leftwall.setAttribute('material', 'color', 'black')
    backwall.setAttribute('material', 'color', 'black')
}

function colored()
{
    var frontleft = document.querySelector('#frontleft')
    var frontright = document.querySelector('#frontright')
    var frontup = document.querySelector('#frontup')
    var frontdown = document.querySelector('#frontdown')
    var ceiling = document.querySelector('#ceiling')
    var floor = document.querySelector('#floor')
    var leftwall = document.querySelector('#leftwall')
    var rightwall = document.querySelector('#rightwall')
    var backwall = document.querySelector('#backwall')

    frontright.setAttribute('material', 'color', 'green')
    frontleft.setAttribute('material', 'color', 'green')
    frontup.setAttribute('material', 'color', 'green')
    frontdown.setAttribute('material', 'color', 'green')
    floor.setAttribute('material', 'color', 'violet')
    ceiling.setAttribute('material', 'color', 'blue')
    rightwall.setAttribute('material', 'color', 'orange')
    leftwall.setAttribute('material', 'color', 'pink')
    backwall.setAttribute('material', 'color', 'brown')
}

// create room
function create()
{
    createWalls()
    setPlaneVisibility(false)
    setWallVisibility(true)

    isRoomCreated = true
}

// create walls
function createWalls()
{
    // just for full view stop
    var rigid = document.querySelector('#roomrig')
    roomRotationY = rigid.object3D.rotation.y

    var frontleft = document.querySelector('#frontleft')
    var frontright = document.querySelector('#frontright')
    var frontup = document.querySelector('#frontup')
    var frontdown = document.querySelector('#frontdown')
    var ceiling = document.querySelector('#ceiling')
    var floor = document.querySelector('#floor')
    var leftwall = document.querySelector('#leftwall')
    var rightwall = document.querySelector('#rightwall')
    var backwall = document.querySelector('#backwall')
    var plane = document.querySelector('#plane')
    var cpivot = document.querySelector('#centerpivot')

    var roomsize = 29
    var width = plane.getAttribute('width')
    var height = plane.getAttribute('height')
    var wspace = roomsize - width
    var hspace = roomsize - height
    var xscale = wspace/2
    var yscale = hspace/2

    frontleft.object3D.position.x = plane.object3D.position.x - (width/2) - (xscale/2)
    frontright.object3D.position.x = plane.object3D.position.x + (width/2) + (xscale/2)
    frontup.object3D.position.y = plane.object3D.position.y + (height/2) + (yscale/2)
    frontdown.object3D.position.y = plane.object3D.position.y - (height/2) - (yscale/2)
    frontup.object3D.position.x = frontdown.object3D.position.x = plane.object3D.position.x
    frontright.object3D.position.y = frontleft.object3D.position.y = plane.object3D.position.y
    frontdown.object3D.position.z = frontup.object3D.position.z = frontright.object3D.position.z = frontleft.object3D.position.z = plane.object3D.position.z
    frontleft.setAttribute('width', xscale)
    frontright.setAttribute('width', xscale)
    frontleft.setAttribute('height', roomsize)
    frontright.setAttribute('height', roomsize)
    frontup.setAttribute('height', yscale)
    frontdown.setAttribute('height', yscale)
    frontup.setAttribute('width', width)
    frontdown.setAttribute('width', width)

    ceiling.object3D.position.x = plane.object3D.position.x
    ceiling.object3D.position.z = plane.object3D.position.z + (roomsize/2)
    ceiling.object3D.position.y = frontup.object3D.position.y + (yscale/2)
    ceiling.setAttribute('width', roomsize)
    ceiling.setAttribute('height', roomsize)

    floor.object3D.position.x = plane.object3D.position.x
    floor.object3D.position.z = plane.object3D.position.z + (roomsize/2)
    floor.object3D.position.y = frontdown.object3D.position.y - (yscale/2)
    floor.setAttribute('width', roomsize)
    floor.setAttribute('height', roomsize)

    rightwall.object3D.position.x = frontright.object3D.position.x + (xscale/2)
    rightwall.object3D.position.z = plane.object3D.position.z + (roomsize/2)
    rightwall.object3D.position.y = plane.object3D.position.y
    rightwall.setAttribute('width', roomsize)
    rightwall.setAttribute('height', roomsize)

    leftwall.object3D.position.x = frontleft.object3D.position.x - (xscale/2)
    leftwall.object3D.position.z = plane.object3D.position.z + (roomsize/2)
    leftwall.object3D.position.y = plane.object3D.position.y
    leftwall.setAttribute('width', roomsize)
    leftwall.setAttribute('height', roomsize)

    backwall.object3D.position.x = plane.object3D.position.x
    backwall.object3D.position.z = plane.object3D.position.z + roomsize
    backwall.object3D.position.y = plane.object3D.position.y
    backwall.setAttribute('width', roomsize)
    backwall.setAttribute('height', roomsize)
}