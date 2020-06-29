#!/bin/bash

# -----------------------------------------------------------------------------
# This script allows you to configure this template.
#
# Example usage:
#   Execute the command below for usage documentation and examples:
#
#   $> bash ./setup.sh --help
# -----------------------------------------------------------------------------

# Forces the execution using bash (as oposed to sh zsh, etc...)
if [ ! "$BASH_VERSION" ] ; then
    exec /bin/bash "$0" "$@"
    exit 0;
fi

# Ensures the script is not executed as root
if [[ $EUID -eq 0 ]]; then
    printf "This script is not intended to be executed as root\n"
    exit 1
fi

# Executes this script in its current location even
# if its being executed from a diferent location
cd $(dirname $0)

# If value is set to yes prevents the script from performing actual modifications.
# This is useful only during development of the script itself.
DRY_RUN=no

# The project name.
PROJECT_NAME=

# The project root namespace.
ROOT_NAMESPACE=

# The database name.
DB_NAME=

# The authentication provider to use. Possible values are 'none', 'lis' ,'custom'
AUTH_PROVIDER=none

# The path of the deployment process log file
LOG_FILE=/var/tmp/setup.log

function write_usage() {
    printf "\n"
    printf "Usage: bash setup.sh <project_name> <root_namespace> <db_name> <auth_provider>\n"
    printf "\n"
    printf "Arguments:\n"
    printf "  - project_name:  The project name.\n"
    printf "                   Example: 'Focus BC - Activities'\n"
    printf "\n"
    printf "  - root_namspace: The project root namespace.\n"
    printf "                   Example: 'Focus.Activities'\n"
    printf "\n"
    printf "  - db_name:       The name of the database. The name will\n"
    printf "                   concatenated with '-dev', '-qa' and '-prod'\n"
    printf "                   for the different environments.\n"
    printf "                   Example: 'activities'\n"
    printf "\n"
    printf "  - auth_provider: The auth provider to use. Possible value\n"
    printf "                   are 'none', 'lis' and 'custom'.\n"
    printf "                   Default is 'custom'.\n"
    printf "\n"
}

function validate_arguments() {
    validate_project_name_argument "$1"
    validate_root_namespace_argument "$2"
    validate_db_name_argument "$3"
    validate_auth_provider_argument "$4"
}

function validate_project_name_argument() {
    arg=$1

    # Show usage information
    if [ "$arg" = "-h" ] || [ "$arg" = "-?" ] || [ "$arg" = "--help" ]; then
        write_usage
        exit 0;
    fi

    # If the action has not been specified as an argument we ask the user
    # to select it. An empty response defaults to "build"
    if [ "$arg" = "" ]; then
        # Asks the user to specify the action to perform if the value was not provided via command line agument
        read -r -p "Please specify the project name [HelloWorld] " PROJECT_NAME
        if [ "$PROJECT_NAME" = "" ]; then
            PROJECT_NAME="HelloWorld"
        fi
    else
        PROJECT_NAME=$arg
    fi
}

function validate_root_namespace_argument() {
    arg=$1

    # If the environment has not been specified as an argument we ask the user
    # to select it. An empty response defaults to "dev"
    if [ "$arg" = "" ]; then
        # Asks the user to specify the environment if the value was not provided via command line agument
        read -r -p "Please specify the root namespace [HelloWorld] " ROOT_NAMESPACE
        if [ "$ROOT_NAMESPACE" = "" ]; then
            ROOT_NAMESPACE="HelloWorld"
        fi
    else
        ROOT_NAMESPACE=$arg
    fi
}

function validate_db_name_argument() {
    arg=$1

    # If the environment has not been specified as an argument we ask the user
    # to select it. An empty response defaults to "db"
    if [ "$arg" = "" ]; then
        # Asks the user to specify the environment if the value was not provided via command line agument
        read -r -p "Please specify the db name [db] " DB_NAME
        if [ "$DB_NAME" = "" ]; then
            DB_NAME="db"
        fi
    else
        DB_NAME=$arg
    fi
}

function validate_auth_provider_argument() {
    arg=$1

    # If the auth provider has not been specified as an argument we ask the user
    # to select it. An empty response defaults to "custom"
    if [ "$arg" = "" ]; then
        # Asks the user to specify the auth provider if the value was not provided via command line agument
        read -r -p "Please specify the auth provider [custom] " AUTH_PROVIDER
        if [ "$AUTH_PROVIDER" = "" ]; then
            AUTH_PROVIDER="custom"
        fi
    else
        AUTH_PROVIDER=$arg
    fi
    if [ ! "$AUTH_PROVIDER" = "none" ] && [ ! "$AUTH_PROVIDER" = "lis" ] && [ ! "$AUTH_PROVIDER" = "custom" ]; then
        printf "\n"
        printf "[\033[1;31mERROR\033[0m] Invalid value for <auth_provider>. Check possible values below.\n"
        write_usage
        exit 1
    fi
}

function print_settings() {

    printf "\n"
    printf "******************************************************\n"
    printf "Confirm settings:\n"
    printf "******************************************************\n"

    printf "  - Log file:       $LOG_FILE\n"
    printf "  - Project name:   $PROJECT_NAME\n"
    printf "  - Root namespace: $ROOT_NAMESPACE.Api\n"
    printf "  - DB name:        $DB_NAME-(dev|qa|prd)\n"
    printf "  - Assembly name:  $(echo "$ROOT_NAMESPACE" | awk '{print tolower($0)}').api.dll\n"
    printf "  - Auth provider:  $AUTH_PROVIDER\n"
    printf "\n"
}

function ask_confirmation() {
    while true; do
        read -r -p "Continue? [y/n] " response
        case $response in
            "" ) continue;;
            [Yy]* ) break;;
            * ) exit 0;;
        esac
    done

    printf "\n"
}

function set_auth_provider() {
    provider=$1

    if [ "$provider" = "none" ]; then
        return
    fi

    if [ "$provider" = "lis" ]; then
        sed -i "" "s/\/\/services.AddLisAuthentication/services.AddLisAuthentication/g" Startup.cs
        sed -i "" "s/\/\/app.UseLisAuthentication/app.UseLisAuthentication/g" Startup.cs
        sed -i "" "s/\/\/\[Authorize\]/\[Authorize\]/g" Infrastructure/BaseController.cs
        sed -i "" "s/abstract class/class/g" Infrastructure/Controllers/AuthenticationController.cs
        return
    fi

    if [ "$provider" = "custom" ]; then
        sed -i "" "s/\/\/services.AddCustomAuthentication/services.AddCustomAuthentication/g" Startup.cs
        sed -i "" "s/\/\/app.UseCustomAuthentication/app.UseCustomAuthentication/g" Startup.cs
        sed -i "" "s/\/\/\[Authorize\]/\[Authorize\]/g" Infrastructure/BaseController.cs
        sed -i "" "s/abstract class/class/g" Infrastructure/Controllers/AuthenticationController.cs
        return
    fi
}

function replace_all() {
    source=$1
    target=$2
    grep -rl "$source" . | xargs sed -i "" "s/$source/$target/g" >> $LOG_FILE 2>&1
}

# tool execution workflow
validate_arguments "$1" "$2" "$3" "$4"
print_settings
ask_confirmation

# Used to calculate how long the script takes to execute. (https://www.safaribooksonline.com/library/view/shell-scripting-expert/9781118166321/c03-anchor-3.xhtml)
SECONDS=0

replace_all "projectName" "$PROJECT_NAME"
replace_all "projectRootNamespace" "$ROOT_NAMESPACE"
replace_all "projectAssemblyName" "$(echo "$ROOT_NAMESPACE" | awk '{print tolower($0)}')"
replace_all "dbName" "$DB_NAME"
set_auth_provider "$AUTH_PROVIDER"

printf "\n"
printf "Process completed \033[1;33m$SECONDS\033[0m seconds!\n"
printf "\n"
