﻿(function (angular) {
    var modalTicketModule = angular.module('ModalTicketModule', ['app.Task.resource','app.ProjectUsers.resource']);

    modalTicketModule.controller('ModalTicketController', function ($scope, $http, close, AuthenticationService, $stateParams,Task,ProjectUsers) {
        
        var userCreatedId = AuthenticationService.getCurrentUser().username;
        var currentProjectId = $stateParams.id;
        var selectedTask = $scope.selectedTask;
        $scope.priorities = [{ value: "Blocker", name: "Blocker" }, { value: "Critical", name: "Critical" }, { value: "Major", name: "Major" }, { value: "Minor", name: "Minor" }, { value: "Trivial", name: "Trivial" }];
        $scope.statuses = [{ value: "To do", name: "To do" }, { value: "In progress", name: "In progress" }, { value: "Verify", name: "Verify" }, { value: "Done", name: "Done" }];
        

        if (!$scope.creation) {
            console.log(selectedTask);
            $scope.ticketName = selectedTask.taskName;
            if (selectedTask.ticketId != null)
            {
                $scope.ticketId = selectedTask.ticketId;
                $scope.ticketAssignedTo = selectedTask.userAssigned;
                $scope.ticketCreatedBy = selectedTask.userCreated;

            }
            else
            {
                $scope.ticketId = selectedTask.ticketID;
                $scope.ticketAssignedTo = selectedTask.userAssignedID;
                $scope.ticketCreatedBy = selectedTask.userCreatedID;
            }
                

            $scope.ticketDescription = selectedTask.taskDescription;
            $scope.ticketPriority = selectedTask.taskPriority;
            $scope.ticketStatus = selectedTask.taskStatus;
          
            $scope.ticketToBeFinishedOn = selectedTask.taskUntil;
            $scope.ticketCreatedOn = selectedTask.taskFrom;
           
            


        }

        $scope.getUsersOnThisTaskProject=function()
        {
            /*$http.get('../../api/projects/' + currentProjectId + '/users')
                .then(function (response) {
                    $scope.usersOnProject = response.data;
                    
            }, function(x) {
                alert('Unable to get users on this project');
            });
            */
            ProjectUsers.getUsersOnProject({projectId:currentProjectId},
                function (response) {
                    $scope.usersOnProject = response;
                },
                function(error)
                {
                    alert('Unable to get users of project');
                }
            );

           
        }
        $scope.getUsersOnThisTaskProject();

        $scope.sendTicket = function () {

            var momentInTime = new Date();
            var ticket = new Task();
            ticket.taskUntil = $scope.ticketAssignedTo;
            ticket.projectId=currentProjectId;
            ticket.taskFrom=momentInTime;
            ticket.taskDescription=$scope.ticketDescription;
            ticket.taskPriority=$scope.ticketPriority;
            ticket.userCreatedId=userCreatedId;
            ticket.taskName=$scope.ticketName;
            ticket.userAssignedId = $scope.ticketAssignedTo;
            ticket.taskStatus = $scope.ticketStatus;
            ticket.taskUntil = $scope.ticketToBeFinishedOn;
            /*
            var data = { "TaskUntil": $scope.ticketToBeFinishedOn, "ProjectID":
                currentProjectId, "TaskFrom": momentInTime,
                "TaskPriority": $scope.ticketPriority, "TaskStatus":
                    $scope.ticketStatus, "UserCreatedId": userCreatedId,
                    "TaskName": $scope.ticketName, "UserAssignedId": $scope.ticketAssignedTo,
                    "TaskDescription": $scope.ticketDescription };
            */
            /*$http.post(
                'api/Projects/' + currentProjectId + '/Tasks',
                JSON.stringify(data),
                {
                    headers: {
                        'Content-Type': 'application/json'
                    }
                }
            ).success(function (data) {
                console.log(data);
                close(data);
                alert("Success");
            }).error(function (error) {
                close('Error');
                alert("Error creating");
            });
            */
            ticket.$save(
             function (data) {
                 close(data);
                 alert("Success !");
             }, function (error) {
                 close('Error');
                 alert("Error creating ticket !");
             });

        }
    
        $scope.validateTicketForm = function () {
            
            if (!$scope.ticketName || !$scope.ticketPriority || !$scope.ticketStatus || !$scope.ticketToBeFinishedOn) {
                return false;
            }
            return true;
        }


        $scope.updateTicket = function () {

            var momentInTime = new Date();
            var dataUpdate = { "TicketID": $scope.ticketId, "TaskUntil": $scope.ticketToBeFinishedOn, "ProjectID": currentProjectId, "TaskFrom": momentInTime, "TaskPriority": $scope.ticketPriority, "TaskStatus": $scope.ticketStatus, "UserCreatedId": $scope.ticketCreatedBy, "TaskName": $scope.ticketName, "UserAssignedId": $scope.ticketAssignedTo, "TaskDescription": $scope.ticketDescription };
            
            /*$http.put(
                'api/Projects/' + currentProjectId + '/Tasks/' + $scope.ticketId,
                JSON.stringify(dataUpdate),
                {
                    headers: {
                        'Content-Type': 'application/json'
                    }
                }
            ).success(function (data) {
                close(data);
                alert('Updated');

            }).error(function (error) {
                close('Error');
                alert("Error updating");
            });
            */
            Task.update({ projectId: currentProjectId, taskId: $scope.ticketId }, dataUpdate,
               function (data) {
                   close(data);
                   alert('Updated !');
               },function (error) {
                   close('Error');
                   alert("Error updating");
               });
                
        }



        $scope.close = function (result) {
            close(result, 200); 
        };

    });
}(angular));