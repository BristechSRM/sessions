Sessions
===
The sessions service only relies on a connection to the mysql database. 

# Running with on Linux / git bash on windows

    1. Edit the app.config file to select the correct connection string, and service url, if required. 
    2. Run `./setup.sh` to install nuget and FAKE relative to the project
    3. Running:
        a. Run `./localBuildRun.sh`
        b. Alternatively, to build and run separately, run `./build.sh`, then `./run.sh`