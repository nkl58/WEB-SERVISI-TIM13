﻿(function (angular) {
    var usersControllerModule = angular.module('app.User.controller', ['angularModalService', 'app.User.resource']);
    
    var usersController = ['$scope', 'User', 'ModalService', function ($scope, User, ModalService) {
        $scope.users = {}

        $scope.selected = null;
        $scope.selectedIndex = null;

        $scope.unselect = function () {
            $scope.selected = null;
            $scope.selectedIndex = null;
        }

        $scope.init = function () {
            var users = User.query(function () {
                $scope.users = users;
            });
        }  

        $scope.selectUser = function (index) {
            if (index != $scope.selectedIndex) {
                $scope.selected = $scope.users[index];
                $scope.selectedIndex = index;
            } else {
                $scope.unselect();
            }     
        }

        $scope.openModal = function (update) {
            ModalService.showModal({
                templateUrl: 'Content/app/users/modal/userModal.html',
                controller: 'userModalController',
                inputs: {
                    selectedUser: update ? $scope.selected : null
                }
            }).then(function (modal) {
                modal.element.modal();
                modal.close.then(function (result) {
                    $('body').removeClass('modal-open');
                    if (result !== 'No' && result !== 'Error') {
                        if (!update) {
                            $scope.users.push(result);
                        } else {
                            $scope.users[$scope.selectedIndex] = result;
                        }
                    }
                });
            });
        }

        $scope.openReportModal = function () {
            ModalService.showModal({
                templateUrl: 'Content/app/users/reports/reportModal.html',
                controller: 'userReportModalController',
                inputs: {
                    selectedUser: $scope.selected
                }
            }).then(function (modal) {
                modal.element.modal();
                modal.close.then(function (result) {
                    $('body').removeClass('modal-open');
                });
            });
        }

        $scope.init();
    }];

    usersControllerModule.controller('usersCtrl', usersController);
})(angular);