﻿(function (angular) {
    var userModalControllerModule = angular.module('app.User.userModalController', ['angularModalService', 'app.User.resource']);

    userModalControllerModule.controller('userModalController', function ($scope, ModalService, selectedUser, User, close, $element) {
        $scope.selectedUser = selectedUser;
        $scope.message = '';
        $scope.unPat = /^[a-z0-9_-]{3,16}$/;
        $scope.emPat = /^([a-z0-9_\.-]+)@([\da-z\.-]+)\.([a-z\.]{2,6})$/;

        if ($scope.selectedUser) {
            $scope.userName = $scope.selectedUser.userName;
            $scope.password = $scope.selectedUser.password;
            $scope.repeatedPassword = $scope.selectedUser.password;
            $scope.firstName = $scope.selectedUser.firstName;
            $scope.lastName = $scope.selectedUser.lastName;
            $scope.email = $scope.selectedUser.email;
            $scope.selectedUser.disable = true;
        }

        $scope.save = function () {
            if (!$scope.selectedUser) {
                var newUser = new User({ userId: $scope.userName });
                newUser.userName = $scope.userName;
                newUser.password = $scope.password;
                newUser.firstName = $scope.firstName;
                newUser.lastName = $scope.lastName;
                newUser.email = $scope.email;

                newUser.$save(
                    function () {
                        $scope.close(newUser);
                        $element.modal('hide');
                    },
                    function (response) {      
                        $scope.message = response.data.message;
                    }
                );
            } else {
                var newUser = {};
                newUser.userName = $scope.userName;
                newUser.password = $scope.password;
                newUser.firstName = $scope.firstName;
                newUser.lastName = $scope.lastName;
                newUser.email = $scope.email;

                User.update({ userId: $scope.userName }, newUser,
                    function () {
                        $element.modal('hide');
                        $scope.close(newUser);
                    },
                    function (response) {
                        $scope.message = response.data.message;
                    }
                );
            }
        }

        $scope.validateForm = function () {

            if (!$scope.userName || !$scope.password || !$scope.repeatedPassword || !$scope.firstName
                || !$scope.lastName || !$scope.email || ($scope.password !== $scope.repeatedPassword) || $scope.password.length < 6
                || !$scope.unPat.test(!$scope.userName) || $scope.emPat.test(!$scope.email)) {
                return false;
            }
            return true;
        }

        $scope.close = function (result) {
            close(result, 200); // close, but give 500ms for bootstrap to animate
        };

    });
}(angular));