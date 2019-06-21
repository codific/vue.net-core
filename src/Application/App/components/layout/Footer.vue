<template>
    <footer id="footer" class="footer bg-vue">
        <div class="container-fluid text-center text-center container pt-4 pb-4">
            <div class="row text-white">
                <div class="col-12 col-md-4 text-left">
                    <h4>Codific Academy</h4>
                    <ul class="text-left m-0 p-0">
                        <li><a target="_blank" href="https://codific.com/about-codific-academy/">About Codific Academy</a></li>
                        <li><a target="_blank" href="https://codific.com/burgas-it-pro-meetup/">Burgas IT Pro Meetup</a></li>
                        <li><a target="_blank" href="https://codific.com/codific-trainings/">Codific Trainings</a></li>
                        <li><a target="_blank" href="https://codific.com/spa-with-net-core-vue-course/">SPA with .Net Core & Vue Course</a></li>
                    </ul>
                </div>
                <div class="col-12 col-md-4 text-left">
                    <h4>Services</h4>
                    <ul class="text-left m-0 p-0">
                        <li><a target="_blank" href="https://codific.com/secure-software-development/">Secure Software Development</a></li>
                        <li><a target="_blank" href="https://codific.com/custom-erp-crm-solutions/">Custom ERP-CRM Solutions</a></li>
                        <li><a target="_blank" href="https://codific.com/privacy-security-and-gdpr/">Privacy, Security and GDPR</a></li>
                    </ul>
                </div>
                <div class="col-12 col-md-4 text-left">
                    <h4>In The Spotlight</h4>
                    <ul class="text-left m-0 p-0">
                        <li><a target="_blank" href="https://codific.com/careers/">Careers</a></li>
                        <li><a target="_blank" href="https://video-lab.eu/">Videolab</a></li>
                        <li><a target="_blank" href="https://codific.com/hr-assessment-tools/">HR Assessment Tools</a></li>
                        <li><a target="_blank" href="https://codific.com/investment-simulation-game/">Investment Simulation Game</a></li>
                    </ul>
                </div>
            </div>
        </div>
        <div class="footer-copyright text-center py-3">
            © {{year}} Copyright
            <a href="https://codific.com"> Codific</a>
        </div>
    </footer>
</template>

<script>
    export default {
        data () {
        return {
            year: (new Date()).getYear() + 1900
        }
      },
        methods: {
        }
    }
</script>

<style>
    #footer {
        background: #282f38;
    }

    #footer .footer-copyright {
        background: #171d27;
        color: #fff;
    }

    #footer .footer-copyright a {
        color: #e74b3d;
        font-weight: bold;
    }

    #footer ul {
        list-style-type: none;
    }
    #footer ul li a {
        color: #fff;
    }
</style>