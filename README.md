# ElderandTest
Elderand Boss for the interview

How it works:
  The project has a few scripts to make the scenery more dynamic, a FSM with Sub-BTs for the FlyingDeepOne's behaviour and a Player Controller.
  The player has 2 layers: one for the world's physics, and a trigger in an interaction layer, for collision with enemies.
  Meanwhile, the enemy only needs one GameObject, using a layer that interacts with the world's physics, but not with the player physics.
  
  The enemy's BT has a FSM with 2 behaviour trees, one for the Ground State and another one for the Flying State. All the values necessary are
  in the Blackboard. Several tasks were made, including one to make the agent move through a bezier curve, using the BezierPath scriptable Object.
