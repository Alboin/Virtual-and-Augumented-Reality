// Class DisplaySurface: Each of the walls of a CAVE.
DisplaySurface = function(orig, uvector, vvector){ // (orig = Vec3, uvector = Vec3, vvector = Vec3)
    
    this.origin = orig; // Vec3 - Origin of the display
    this.u = uvector;   // Vec3 - Horizontal vector
    this.v = vvector;   // Vec3 - Vertical vector

};

// Functions

DisplaySurface.prototype.viewingMatrix = function(eye){
	var mat = new Mat4();

	var eye_to_point = Vec3.subtract(this.origin, eye);

	var normal = Vec3.cross(this.u, this.v);

	eye_to_point = normal.project(eye_to_point);

	mat.lookAt(eye, Vec3.add(eye_to_point, eye), this.v);

	return mat;
};

DisplaySurface.prototype.projectionMatrix = function(eye, znear, zfar){
    var mat = new Mat4();

    //Calculate right
    var A = eye;
    var B = Vec3.add(this.origin, this.u);
    var AB = Vec3.subtract(B, A);
    AB = Vec3.subtract(AB, this.v.project(AB));

    var n = Vec3.cross(this.u, this.v);
    n = n.normalize();
    var ABn = n.project(AB);
    var D = ABn.norm();
    var questionmark = Vec3.subtract(AB, ABn);
    var l_questionmark = questionmark.norm();

    var right = znear * l_questionmark / D;

    //Calculate left (A and n is the same for all)
    var B = this.origin;
    var AB = Vec3.subtract(B, A);
    AB = Vec3.subtract(AB, this.v.project(AB));
    var ABn = n.project(AB);
    var D = ABn.norm();
    var questionmark = Vec3.subtract(AB, ABn);
    var l_questionmark = questionmark.norm();

    var left = -znear * l_questionmark / D;

    //Calculate top
    var B = Vec3.add(this.origin, this.v);
    var AB = Vec3.subtract(B, A);
    AB = Vec3.subtract(AB, this.u.project(AB));

    var ABn = n.project(AB);
    var D = ABn.norm();
    var questionmark = Vec3.subtract(AB, ABn);
    var l_questionmark = questionmark.norm();
    var top = znear * l_questionmark / D;

    //Calculate bottom
    var B = this.origin;
    var AB = Vec3.subtract(B, A);
    AB = Vec3.subtract(AB, this.u.project(AB));

    var ABn = n.project(AB);
    var D = ABn.norm();
    var questionmark = Vec3.subtract(AB, ABn);
    var l_questionmark = questionmark.norm();

    var bottom = -znear * l_questionmark / D;

    mat.frustum(left, right, bottom, top, znear, zfar);

	return mat;
};
