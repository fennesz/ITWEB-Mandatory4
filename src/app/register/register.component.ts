





module.exports.register = function(req, res){
    if(!req.body.name || !req.body.email || !req.body.password){
        res
            .status(400)
            .json({"message": "All fields required"});
        return;
    }
    const user = new user();
    user.name = req.body.name;
    user.email = req.body.name;
    user.setPassword(req.body.password);
    user.save(function(err){
        var token;
        if(err){
            res
                .status(404)
                .json({"message": err});
        } else {
            token = user.generateJwt();
            res
                .status(200)
                .json({"token" : token});
        }
    });
};